# FMFI Blazor app

This is a .NET 10 Blazor Server app. Its GitHub Actions workflow is in [`.github/workflows/azure-app-service.yml`](.github/workflows/azure-app-service.yml).

## Build and deployment

- Pull requests targeting `dev` build and run tests without deploying.
- Pushes to `dev` build, test, publish, and deploy automatically to the `development` Azure App Service.
- To deploy to `production`, run **Actions → Build and deploy to Azure App Service → Run workflow**, select the `dev` branch and `production`. The same workflow builds and tests that revision before deploying it. You can also choose `development` to redeploy manually.

The deployment job uses GitHub's `development` or `production` environment and Azure OpenID Connect (OIDC) authentication. The published app is transferred between the build and deploy jobs as a workflow artifact.

## One-time Azure and GitHub setup

1. Create or select two Azure App Service web apps that support ASP.NET Core **.NET 10**. The sibling Azure DevOps pipeline used `webapp-devopsfmfi2025` for development and `FMFITEST` for production; verify that these are the intended targets before using them here.
2. In GitHub repository **Settings → Environments**, create environments named `development` and `production`. For each environment, add the variable `AZURE_WEBAPP_NAME` with that environment's exact Azure web app name. If your GitHub plan supports it, add required reviewers to `production` for a separate approval gate. Restrict both environments to the `dev` branch.
3. Create a Microsoft Entra application/service principal or a user-assigned managed identity with **Website Contributor** access scoped to each target web app. Configure a federated credential for each GitHub environment, with issuer `https://token.actions.githubusercontent.com`, audience `api://AzureADTokenExchange`, and these exact subjects:

   ```text
   repo:fmfidevops2025/FMFI:environment:development
   repo:fmfidevops2025/FMFI:environment:production
   ```

   If using separate identities, give each identity access only to its own web app and configure its corresponding federated credential.
4. In each GitHub environment, add secrets `AZURE_CLIENT_ID`, `AZURE_TENANT_ID`, and `AZURE_SUBSCRIPTION_ID` for that environment's identity and subscription. These are the application or managed identity client ID, Microsoft Entra tenant ID, and Azure subscription ID. The Azure DevOps service connection from the sibling project does not authenticate GitHub Actions.
5. In each Azure App Service, set `ConnectionStrings__DefaultConnection` as an application setting containing that environment's MySQL connection string. Ensure Azure can reach the database host. Keep this value out of GitHub and the workflow artifact.

The old database password was committed in source history. Rotate it before deploying and use the replacement only in App Service settings. Removing it from the current code does not remove it from Git history.

See the [Azure OIDC setup guide](https://learn.microsoft.com/azure/developer/github/connect-from-azure-openid-connect) and [GitHub environment setup guide](https://docs.github.com/actions/how-tos/deploy/configure-and-manage-deployments/manage-environments) for the portal steps.

## Local development

Set `ConnectionStrings__DefaultConnection` in your local environment, then run:

```powershell
dotnet restore MyBlazorApp.sln
dotnet test MyBlazorApp.sln
dotnet run --project MyBlazorApp.csproj
```
