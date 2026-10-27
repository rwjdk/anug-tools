# ANUG Tools

## Run locally

All projects enable nullable reference checking and treat warnings as errors
through `Directory.Build.props`. CI also enforces MSBuild warnings as errors.

Install the .NET 10 SDK and Node.js, then install the local tools:

```powershell
npm install -g azure-functions-core-tools@4 azurite
```

Trust the development HTTPS certificate with `dotnet dev-certs https --trust`.

From the repository root, run:

```powershell
dotnet run --project AppHost
```

Aspire starts Azurite, the Functions backend, and Blazor frontend as local processes (no Docker) and provides
a dashboard with their status and logs. Open the frontend at
`https://localhost:7112`. Development configuration uses the local backend
at `http://localhost:7083`; production configuration uses the hosted Azure API.
Local Functions CORS is restricted to the frontend's HTTP and HTTPS origins.

## Deploy to Azure

`.github/workflows/deploy.yml` builds and tests pull requests. Merging to `main`
builds, tests, and deploys the Functions backend to `AnugToolsBackend`, then
the Blazor publish directory (including IIS `web.config`) to `AnugTools`.
The workflow can also be run manually on `main` from GitHub Actions.

Azure authentication uses OIDC, with no publish-profile password. The
`anug-tools-github-deploy` managed identity trusts only
`repo:rwjdk@7032102/anug-tools@641368448:ref:refs/heads/main` and has Website Contributor access
only on those two apps. GitHub repository variables `AZURE_CLIENT_ID`,
`AZURE_TENANT_ID`, and `AZURE_SUBSCRIPTION_ID` supply the identity IDs.

The backend runs on Azure Functions v4 with `.NET 10 Isolated`. The frontend
is static Blazor WebAssembly served by IIS; it doesn't execute a server-side
.NET application. Aspire and Azurite are for local development only.

Hi 👋, this is the repo of a small Blazor App to pick price Winners and other tools to conduct meetings in the [Aarhus .NET User Group](https://meetup.com/anugdk) (ANUG)

![alt text](Images/anug.png)

*Your friendly local .NET nuts in Aarhus... We love everything .NET* 😀
