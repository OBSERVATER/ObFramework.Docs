# Business generator diagnostics

All descriptors below use category `ObFramework.CDE`. OBF138 is a warning; the remaining listed rules are errors. The authoritative descriptor messages live in [CdeDiagnostics.cs](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Generator/Integrated/CDE/CdeDiagnostics.cs).

| ID | Trigger | Typical correction |
|---|---|---|
| OBF100 | Owning View declaration is not partial | Declare every source class part `partial` |
| OBF101 | Owning type does not derive from `AUIView` | Put business state/commands on a business View |
| OBF102 | View is nested | Move it to a top-level class |
| OBF103 | View has type parameters | Use a non-generic View |
| OBF104 | Owning type is not a class | Use an `AUIView` class |
| OBF110 | Observable field is static | Use per-View instance state |
| OBF111 | Observable field is const | Use mutable state |
| OBF112 | Observable field is readonly | Use a writable backing field |
| OBF113 | Observable field type cannot be used | Use a regular non-pointer, non-ref-like type |
| OBF114 | Generated property/event collides with an existing member | Rename the field or conflicting member |
| OBF115 | Fields derive the same property name | Choose distinct derived names |
| OBF116 | Field name cannot produce a valid property identifier | Use a name such as `_count` |
| OBF120 | Dependent property/command target is empty | Supply a non-empty name, preferably `nameof(...)` |
| OBF130 | Relay target is not an ordinary method | Use a normal instance method |
| OBF131 | Relay method is static | Use an instance method |
| OBF132 | Relay method is generic | Use a non-generic method |
| OBF133 | Relay signature has unsupported parameters | Use at most one event value and an optional trailing async cancellation token |
| OBF134 | Relay return type is unsupported | Return `void`, `Task` or `ValueTask` |
| OBF135 | Two methods derive the same command type name | Rename one; account for removal of trailing `Async` |
| OBF136 | Named CanExecute method cannot be found | Correct the method name or add the predicate |
| OBF137 | CanExecute signature/accessibility is unsupported or ambiguous | Supply one accessible matching instance `bool` predicate |
| OBF138 | Async-only option is set on a void relay method | Remove the option or return Task/ValueTask |
| OBF139 | Generated command type collides with another member | Rename the method or conflicting member |

Unity binding diagnostics have different ranges and live in the [Unity generator project](../ObFramework.Generator.Unity/README.md). Save generation currently has no corresponding diagnostic range.
