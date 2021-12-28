using System;

namespace Dashboard.API.CDK
{
    using JustLogin.Core.CDK.Builder;

    sealed class Program
    {
        public static void Main(string[] args)
        {
            new CDKBuilder()
                .InitializeConfiguration("Configs")
                .ConfigureServices()
                .Build("dashboard-api");
        }
    }
}
