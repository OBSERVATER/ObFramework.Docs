# Unity generator diagnostics

All listed Unity generator diagnostics are errors. `OBFV` belongs to category `ViewBinding`; `OBFU` belongs to `UnityUIBinding`.

| ID | Trigger | Typical correction |
|---|---|---|
| OBFV001 | Invalid controller shape or existing inherited/generated binding | Use one concrete top-level non-generic partial MonoBehaviour binding |
| OBFV002 | Contract is not an accessible interface implemented by the controller | Correct BindView's contract and controller interface list |
| OBFV003 | Business View is invalid or does not have exactly one public constructor | Use a concrete accessible non-Unity class and one public constructor |
| OBFV004 | Constructor dependency is unsupported or ambiguous | Use the contract, IArchitecture, one system/model category, or a supported concrete plugin dependency |
| OBFV005 | Controller/base already declares a reserved Unity callback | Move business behavior to the supported View lifecycle; leave generated callbacks available |
| OBFU101 | Control type cannot perform the requested data/command binding | Use a control from the supported table |
| OBFU102 | View source/member/command declaration cannot be found | Include source in the same compilation and correct the target name |
| OBFU103 | Display-only control is configured TwoWay | Use OneWay or an input control |
| OBFU104 | Event/property value conversion is unsupported | Align the property/command parameter type with the control's event value |
| OBFU105 | Field has both binding attributes, or several TwoWay controls write a property | Give the binding one unambiguous writer |
| OBFU106 | Controller has binding fields but no BindView attribute | Add the View and contract binding |
| OBFU107 | Empty/invalid target or binding mode argument | Use a correct member name and supported enum value |
| OBFU108 | Invalid binding declaration, setter, business View or hand-written command shape | Follow the declared runtime binding contract |

The separate [business generator diagnostics](../ObFramework.Generator/diagnostics.md) describe invalid observable/relay declarations. A Unity binding diagnostic number is a stable rule identifier, not a count of errors.
