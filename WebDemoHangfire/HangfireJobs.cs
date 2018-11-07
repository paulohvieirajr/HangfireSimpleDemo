using Hangfire;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemoHangfire
{
    public class HangfireJobs
    {
        public static void Register()
        {
            // Executado apenas uma vez quando chamado. "Fire and Forget"
            var jobFireForget = BackgroundJob.Enqueue(() => Debug.WriteLine($"Fire and forget: {DateTime.Now}"));

            // Job executado em um momento específico... 
            var jobDelayedEspecificTime = BackgroundJob.Schedule(() => Debug.WriteLine($"Delayed: {DateTime.Now}"), DateTimeOffset.Now.AddSeconds(2));
            // ou em quando tempo após criado, que deve ser executado.
            var jobDelayed = BackgroundJob.Schedule(() => Debug.WriteLine($"Delayed: {DateTime.Now}"), TimeSpan.FromSeconds(30));

            // Executa um job após o termino de outro. Assim você cria a execução em sequ~encia de vários passos para o processamento de algumas informações. 
            BackgroundJob.ContinueWith(jobDelayed, () => Debug.WriteLine($"Continuation: {DateTime.Now}"));

            // Executa um job recorrentemente a cada x espaço de tempo. 
            RecurringJob.AddOrUpdate(() => Debug.WriteLine($"Recurring: {DateTime.Now}"), Cron.Minutely);
        }
    }
}
