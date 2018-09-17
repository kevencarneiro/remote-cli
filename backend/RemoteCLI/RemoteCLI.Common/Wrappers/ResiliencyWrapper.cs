using System;
using System.Threading.Tasks;
using Polly;

namespace RemoteCLI.Common.Wrappers
{
    public static class Resiliency
    {
        private static TimeSpan ExponentialBackoff(int retryAttempt)
            => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));

        public static T ExecuteOrRetry<T>(Func<T> func)
            => Policy.Handle<Exception>()
            .WaitAndRetryForever(attempt => ExponentialBackoff(attempt))
            .Execute(func);

        public static Task ExecuteOrRetryAsync(Func<Task> func)
            => Policy.Handle<Exception>()
            .WaitAndRetryForeverAsync(attempt => ExponentialBackoff(attempt))
            .ExecuteAsync(func);

        public static Task<T> ExecuteOrRetryAsync<T>(Func<Task<T>> func)
            => Policy.Handle<Exception>()
            .WaitAndRetryForeverAsync(attempt => ExponentialBackoff(attempt))
            .ExecuteAsync(func);
    }
}