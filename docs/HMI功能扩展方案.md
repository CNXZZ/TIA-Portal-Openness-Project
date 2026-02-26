# HMI 功能扩展方案（可行）

## 结论
当前项目**可以增加**以下 3 类 HMI 能力：
1. HMI 画面设计（基础画面对象自动创建/布局）
2. HMI 变量编辑（HMI Tag 批量创建、修改、绑定 PLC 变量）
3. HMI 文本列表编辑（TextList 的多语言条目读写）

项目已引用 `Siemens.Engineering.Hmi.dll`，并在核心接口文件中引入了 HMI 相关命名空间，具备继续扩展的技术基础。

## 建议的最小可交付范围（MVP）

### 1) HMI 变量编辑
- 输入：变量名、数据类型、PLC 连接、地址/符号名、采集周期。
- 输出：自动创建或更新 HMI Tags。
- 校验：重名检查、非法字符检查、地址范围检查。

### 2) HMI 文本列表编辑
- 输入：文本列表名、条目 Key、语言（如 `zh-CN`/`en-US`）、文本值。
- 输出：自动创建 TextList，并写入多语言内容。
- 校验：Key 唯一、语言存在性、空文本提示。

### 3) HMI 画面设计
- 输入：画面名、对象类型（按钮/IO 域/文本）、坐标和尺寸、绑定变量。
- 输出：自动生成基础画面与对象，完成变量绑定。
- 校验：画布越界、对象重叠（可先只做提示）。

## 推荐实施顺序
1. 先做 **HMI 变量编辑**（风险最低、复用度最高）。
2. 再做 **文本列表编辑**（与多语言相关，依赖变量模型不强）。
3. 最后做 **画面设计**（对象模型复杂、测试成本最高）。

## 代码落地建议
- 在 `Common/Models` 下新增 HMI DTO（Tag/TextList/ScreenObject）。
- 在 `Common/Interfaces/TIA_Portal.cs` 中补充 HMI 的 CRUD 方法：
  - `CreateOrUpdateHmiTags(...)`
  - `CreateOrUpdateTextList(...)`
  - `CreateBasicScreen(...)`
- 在 `ViewModels/StartTIAPortalViewModel.cs` 中增加 3 个入口命令和执行流程。
- 在 `Views/TransitionView.xaml`（或新建 HMI 配置页）增加对应配置项和校验提示。

## 风险点
- 不同 TIA 版本 HMI API 细节有差异，建议对 V17/V18/V19 做兼容分支。
- 画面对象模板差异较大（Unified / Comfort），建议先锁定一种面板类型。
- 大批量写入时需注意 UI 线程与后台任务切换，避免界面卡顿。

## 验收标准（建议）
- 能批量创建 ≥100 个 HMI 变量并完成 PLC 绑定。
- 能创建文本列表并写入至少 2 种语言。
- 能自动生成 1 个画面，且包含至少 3 个绑定对象（按钮/IO 域/文本）。
