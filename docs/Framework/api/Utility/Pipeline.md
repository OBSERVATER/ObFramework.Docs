# Framework / Utility/Pipeline

[API index](../README.md)

## Utility/Pipeline/IPipelineMiddleware.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Utility/Pipeline/IPipelineMiddleware.cs)

### ObFramework.Utility.Pipeline.IPipelineMiddleware<TContext>

Declared at [line 3](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Utility/Pipeline/IPipelineMiddleware.cs#L3).

```csharp
public interface IPipelineMiddleware<TContext>
    void Invoke(ref TContext context, ref PipelineNext<TContext> next);
```

## Utility/Pipeline/IPipelineStep.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Utility/Pipeline/IPipelineStep.cs)

### ObFramework.Utility.Pipeline.IPipelineStep<TContext>

Declared at [line 3](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Utility/Pipeline/IPipelineStep.cs#L3).

```csharp
public interface IPipelineStep<TContext>
    StepResult Execute(ref TContext context);
```

## Utility/Pipeline/Pipeline.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Utility/Pipeline/Pipeline.cs)

### ObFramework.Utility.Pipeline.Pipeline<TContext>

Declared at [line 6](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Utility/Pipeline/Pipeline.cs#L6).

```csharp
public readonly struct Pipeline<TContext>
    public TContext Execute(TContext context);
    public void Execute(ref TContext context);
```

## Utility/Pipeline/PipelineBuilder.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Utility/Pipeline/PipelineBuilder.cs)

### ObFramework.Utility.Pipeline.PipelineBuilder<TContext>

Declared at [line 5](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Utility/Pipeline/PipelineBuilder.cs#L5).

```csharp
public sealed class PipelineBuilder<TContext>
    public PipelineBuilder<TContext> Add(IPipelineStep<TContext> step);
    public PipelineBuilder<TContext> Use(IPipelineMiddleware<TContext> middleware);
    public Pipeline<TContext> Build();
```

## Utility/Pipeline/PipelineNext.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Utility/Pipeline/PipelineNext.cs)

### ObFramework.Utility.Pipeline.PipelineNext<TContext>

Declared at [line 3](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Utility/Pipeline/PipelineNext.cs#L3).

```csharp
public class PipelineNext<TContext>
    public PipelineNext(Pipeline<TContext> pipeline, int v);
    public void Invoke(ref TContext context);
```

## Utility/Pipeline/StepResult.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Utility/Pipeline/StepResult.cs)

### ObFramework.Utility.Pipeline.StepResult

Declared at [line 3](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Utility/Pipeline/StepResult.cs#L3).

```csharp
public enum StepResult : byte
    Continue,
    Stop,
```
