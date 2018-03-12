using System;
using System.Threading.Tasks;

namespace Controls14.Activation
{
    // For more information on application activation see https://github.com/Microsoft/WindowsTemplateStudio/blob/master/docs/activation.md
    internal abstract class ActivationHandler
    {
        public abstract bool CanHandle(object args);

        public abstract Task HandleAsync(object args);
    }

    internal abstract class ActivationHandler<T> : ActivationHandler
        where T : class
    {
        protected abstract Task HandleInternalAsync(T args);

        public override async Task HandleAsync(object args)
        {
#pragma warning disable IDE0022 // Use expression body for methods
            await HandleInternalAsync(args as T);
#pragma warning restore IDE0022 // Use expression body for methods
        }

        public override bool CanHandle(object args)
        {
#pragma warning disable IDE0022 // Use expression body for methods
            return args is T && CanHandleInternal(args as T);
#pragma warning restore IDE0022 // Use expression body for methods
        }

        protected virtual bool CanHandleInternal(T args)
        {
#pragma warning disable IDE0022 // Use expression body for methods
            return true;
#pragma warning restore IDE0022 // Use expression body for methods
        }
    }
}
