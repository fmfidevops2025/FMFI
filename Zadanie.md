Zadanie DevOps Cvičenia – Aktualizovaná verzia 2025
Prerequisites

- Konto a free subscription na Microsoft Azure (Azure for Students alebo Free Trial)
- Organizácia v Azure DevOps (Free účet, školský email odporúčaný)

Cieľ zadania

Cieľom je vytvoriť moderné DevOps prostredie pre vývoj aplikácie, kde bude tím:
- plánovať prácu,
- spravovať zdrojový kód,
- využívať CI/CD pipeline,
- automaticky nasadzovať aplikáciu do Azure App Service.

**📝 Poznámka o pipeline prístupoch (2025):**

V tomto zadaní sú popísané **dva prístupy** k CI/CD:

1. **Jednoduchší prístup (pre začiatočníkov):**
   - CI pipeline (YAML) - bod 4
   - Release Pipeline (Classic UI) - bod 6
   - PR validation pipeline (YAML) - bod 8

2. **Pokročilejší prístup (moderný, odporúčaný):**
   - Multi-stage YAML pipeline (kombinuje body 4 a 6 do jedného súboru)
   - PR validation pipeline (YAML) - bod 8
   - Používa Azure Environments a approval gates

**Ak ste pokročilí**, použite multi-stage YAML (viď poznámku pri bode 6). Ak **začínate**, pokračujte s Classic Release Pipeline - je to jednoduchšie na pochopenie konceptov.

**Demo projekt MyBlazorApp používa pokročilý multi-stage prístup.**

---

Cvičenie 1 – Vytvorenie prostredia (Aktualizované)
1. Vytvorenie DevOps projektu (1B)

- V Azure DevOps vytvorte nový projekt (vyhnite sa medzerám a špeciálnym znakom).
- Inicializujte repozitár a naklonujte si ho lokálne.

**Postupné kroky pre študentov:**

1.1. Registrácia a prihlásenie do Azure DevOps
- Otvorte https://dev.azure.com
- Prihláste sa s vaším Microsoft účtom (odporúčaný školský email)
- Ak nemáte organizáciu, vytvorte novú

1.2. Vytvorenie organizácie (ak ešte nemáte)
- Kliknite na "Create new organization"
- Pomenujte organizáciu (napr. "FMFIDevOps" alebo "VaseMeno-DevOps")
- Vyberte región (West Europe)
- Kliknite "Continue"

1.3. Vytvorenie nového projektu
- V organizácii kliknite na "New project"
- Project name: zadajte názov BEZ medzier a špeciálnych znakov
  - ✅ Správne: "FMFIBlazorApp", "DevOpsProject2025"
  - ❌ Nesprávne: "FMFI Blazor App!", "Projekt-Číslo-1"
- Description: popíšte projekt (voliteľné)
- Visibility: "Private" (odporúčané)
- Advanced:
  - Version control: "Git"
  - Work item process: "Agile" alebo "Scrum"
- Kliknite "Create"

1.4. Inicializácia repozitára
- Po vytvorení projektu prejdite do Repos
- Ak je repozitár prázdny, kliknite na "Initialize" alebo "Add a README"
- Zaškrtnite "Add a README" (odporúčané)
- Kliknite "Initialize"

1.5. Získanie Git URL
- V Repos kliknite na "Clone" (vpravo hore)
- Skopírujte HTTPS URL (napr. `https://dev.azure.com/org/project/_git/repo`)

1.6. Klonovanie repozitára lokálne
```bash
# Otvorte terminál / command prompt
# Prejdite do priečinka, kde chcete mať projekt

# Klonujte repozitár
git clone https://dev.azure.com/[org]/[project]/_git/[repo]

# Pri výzve sa prihláste do Azure DevOps
# Môžete použiť Personal Access Token (PAT) alebo credential manager

# Prejdite do priečinka projektu
cd [repo-name]

# Overte, že ste na main vetve
git branch
```

1.7. Nastavenie Git používateľa (ak ešte nie je)
```bash
git config --global user.name "Vaše Meno"
git config --global user.email "vas.email@example.com"
```

2. Push aplikácie do repozitára (1B)

Nahrajte vlastnú aplikáciu (napr. jednoduchý .NET project/Blazor aplikaciu co ste robili s kolegom M.Cibulom alebo akýkoľvek iný jazyk).

**Postupné kroky pre študentov:**

2.1. Vytvorenie alebo príprava aplikácie
- Ak už máte existujúcu aplikáciu, pripravte si ju
- Ak nemáte aplikáciu, vytvorte novú:

**Pre .NET/Blazor aplikáciu:**
```bash
# Vytvorte novú Blazor Server aplikáciu
dotnet new blazorserver -n FMFIBlazorApp

# Alebo Blazor WebAssembly
dotnet new blazorwasm -n FMFIBlazorApp

# Prejdite do priečinka aplikácie
cd FMFIBlazorApp

# Overte, že aplikácia funguje
dotnet build
dotnet run
```

**Pre Node.js aplikáciu:**
```bash
# Vytvorte novú aplikáciu (napr. Express)
npm init -y
npm install express
# Vytvorte index.js s jednoduchým serverom
```

2.2. Skopírovanie aplikácie do Git repozitára
```bash
# Prejdite do naklonovaného Azure DevOps repozitára (z bodu 1)
cd /cesta/k/vašmu/repo

# Skopírujte súbory aplikácie do repozitára
# Windows:
xcopy /E /I C:\cesta\k\aplikácii\* .

# Linux/Mac:
cp -r /cesta/k/aplikácii/* .

# Alebo vytvorte aplikáciu priamo v repozitári
dotnet new blazorserver -n FMFIBlazorApp
mv FMFIBlazorApp/* .
```

2.3. Vytvorenie .gitignore súboru
- Je dôležité pridať .gitignore, aby ste necommitovali zbytočné súbory
```bash
# Pre .NET projekty
curl -o .gitignore https://raw.githubusercontent.com/github/gitignore/main/VisualStudio.gitignore

# Alebo vytvorte .gitignore manuálne s obsahom:
```

**.gitignore pre .NET:**
```
bin/
obj/
.vs/
*.user
*.suo
*.cache
*.log
```

2.4. Commit a push aplikácie
```bash
# Skontrolujte status
git status

# Pridajte všetky súbory
git add .

# Vytvorte commit
git commit -m "Add initial Blazor application"

# Pushnite do Azure DevOps
git push origin main
```

2.5. Overenie v Azure DevOps
- Otvorte Azure DevOps v prehliadači
- Prejdite do Repos → Files
- Mali by ste vidieť všetky súbory vašej aplikácie
- Skontrolujte, že sa tam nenachádzajú `bin/` a `obj/` priečinky (ak áno, skontrolujte .gitignore)

2.6. Poznámky
- Ak máte problém s autentifikáciou pri push:
  - Vytvorte Personal Access Token (PAT) v Azure DevOps
  - Project Settings → Personal Access Tokens → New Token
  - Scope: "Code (Read & Write)"
  - Použite PAT ako heslo pri git push

3. Pridanie testov (1B)

Pridajte jednoduchý testovací projekt.
Môžu to byť aj „fake" testy – cieľ je ukázať fungujúci testovací krok.

**Postupné kroky pre študentov:**

3.1. Vytvorenie testovacieho projektu (.NET)
```bash
# Prejdite do root priečinka vášho repozitára
cd /cesta/k/repo

# Vytvorte testovací projekt (xUnit)
dotnet new xunit -n FMFIBlazorApp.Tests

# Alebo NUnit
dotnet new nunit -n FMFIBlazorApp.Tests

# Alebo MSTest
dotnet new mstest -n FMFIBlazorApp.Tests
```

3.2. Pridanie referencie na hlavnú aplikáciu
```bash
# Prejdite do testovacieho projektu
cd FMFIBlazorApp.Tests

# Pridajte referenciu na hlavný projekt
dotnet add reference ../FMFIBlazorApp.csproj

# Návrat do root priečinka
cd ..
```

3.3. Vytvorenie solution súboru (voliteľné, ale odporúčané)
```bash
# Vytvorte solution súbor
dotnet new sln -n FMFIBlazorApp

# Pridajte projekty do solution
dotnet sln add FMFIBlazorApp.csproj
dotnet sln add FMFIBlazorApp.Tests/FMFIBlazorApp.Tests.csproj

# Overte štruktúru
dotnet sln list
```

3.4. Napísanie jednoduchého testu
- Otvorte súbor v testovom projekte: `FMFIBlazorApp.Tests/UnitTest1.cs`
- Nahraďte obsah jednoduchým testom:

**Príklad pre xUnit:**
```csharp
using Xunit;

namespace FMFIBlazorApp.Tests
{
    public class BasicTests
    {
        [Fact]
        public void SimpleTest_ShouldPass()
        {
            // Arrange
            int a = 2;
            int b = 3;

            // Act
            int result = a + b;

            // Assert
            Assert.Equal(5, result);
        }

        [Fact]
        public void StringTest_ShouldPass()
        {
            // Arrange
            string expected = "DevOps";

            // Act
            string actual = "DevOps";

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
```

3.5. Spustenie testov lokálne
```bash
# Spustite všetky testy
dotnet test

# Mali by ste vidieť výstup:
# Passed!  - Failed:     0, Passed:     2, Skipped:     0, Total:     2
```

3.6. (Voliteľné) Pridanie komplexnejšieho testu
- Vytvorte novú triedu s jednoduchým funkcionalitou v hlavnom projekte:

**Calculator.cs v hlavnom projekte:**
```csharp
namespace FMFIBlazorApp
{
    public class Calculator
    {
        public int Add(int a, int b) => a + b;
        public int Subtract(int a, int b) => a - b;
        public int Multiply(int a, int b) => a * b;
        public int Divide(int a, int b) => a / b;
    }
}
```

**CalculatorTests.cs v testovom projekte:**
```csharp
using Xunit;
using FMFIBlazorApp;

namespace FMFIBlazorApp.Tests
{
    public class CalculatorTests
    {
        private readonly Calculator _calculator;

        public CalculatorTests()
        {
            _calculator = new Calculator();
        }

        [Fact]
        public void Add_TwoNumbers_ReturnsSum()
        {
            // Act
            var result = _calculator.Add(5, 3);

            // Assert
            Assert.Equal(8, result);
        }

        [Fact]
        public void Multiply_TwoNumbers_ReturnsProduct()
        {
            // Act
            var result = _calculator.Multiply(4, 5);

            // Assert
            Assert.Equal(20, result);
        }
    }
}
```

3.7. Commit a push testov
```bash
# Overte status
git status

# Pridajte všetky súbory
git add .

# Commit
git commit -m "Add unit tests project"

# Push do Azure DevOps
git push origin main
```

3.8. Overenie v Azure DevOps
- Prejdite do Repos → Files
- Mali by ste vidieť:
  - Hlavný projekt (FMFIBlazorApp.csproj)
  - Testovací projekt (FMFIBlazorApp.Tests/)
  - Solution súbor (FMFIBlazorApp.sln)

3.9. Poznámky pre študentov
- Na účely cvičenia sú "fake" testy úplne v poriadku
- Dôležité je, aby:
  - ✅ Testy sa kompilovali bez chýb
  - ✅ Testy prechádzali (zelené)
  - ✅ Pipeline vedela spustiť `dotnet test`
- V produkčnom prostredí by ste mali písať skutočné unit testy pre vašu business logiku

4. Build pipeline (CI) – YAML (1B)

Build pipeline musí obsahovať:
- dotnet restore
- dotnet build
- dotnet test
- dotnet publish (ak nasadzujete .NET app)

Pipeline beží automaticky pri commite do hlavnej vetvy.

**Postupné kroky pre študentov:**

4.1. Vytvorenie novej pipeline
- V Azure DevOps prejdite do Pipelines → Create Pipeline
- Vyberte "Azure Repos Git" ako zdroj kódu
- Vyberte váš repozitár

4.2. Výber šablóny
- Vyberte "Starter pipeline" alebo ".NET Desktop" (podľa typu projektu)
- Otvorí sa YAML editor

4.3. Úprava YAML súboru
- Nastavte trigger na main vetvu
- Pridajte pool (napr. ubuntu-latest alebo windows-latest)
- Pridajte kroky (steps) pre restore, build, test, publish

**💡 Tip:** V repozitári nájdete dva vzorové súbory:
- `azure-pipelines-simple.yml` - **kompletný multi-stage príklad** (Build + Deploy) - ideálny ak chcete rovno CI/CD
- Príklad nižšie - **len CI** (bez deploymentu) - jednoduchší na pochopenie základov

**Odporúčame začať s príkladom nižšie (len CI), a potom prejsť na multi-stage v bode 6.**

Príklad YAML (len CI - bez deploymentu):
```yaml
trigger:
  branches:
    include:
    - main

pool:
  vmImage: 'ubuntu-latest'

steps:
- task: UseDotNet@2
  displayName: 'Inštalácia .NET SDK'
  inputs:
    version: '10.x'

- task: DotNetCoreCLI@2
  displayName: 'Restore dependencies'
  inputs:
    command: 'restore'
    projects: '**/*.csproj'

- task: DotNetCoreCLI@2
  displayName: 'Build project'
  inputs:
    command: 'build'
    projects: '**/*.csproj'
    arguments: '--configuration Release'

- task: DotNetCoreCLI@2
  displayName: 'Run tests'
  inputs:
    command: 'test'
    projects: '**/*Tests.csproj'
    arguments: '--configuration Release --no-build --collect:"XPlat Code Coverage"'
    publishTestResults: true  # Automaticky publikuje test results aj code coverage do Azure DevOps

- task: DotNetCoreCLI@2
  displayName: 'Publish application'
  inputs:
    command: 'publish'
    publishWebProjects: true
    arguments: '--configuration Release --output $(Build.ArtifactStagingDirectory)'
    zipAfterPublish: true

- task: PublishBuildArtifacts@1
  displayName: 'Publish artifacts'
  inputs:
    PathtoPublish: '$(Build.ArtifactStagingDirectory)'
    ArtifactName: 'drop'
```

4.4. Uloženie a spustenie
- Kliknite na "Save and run"
- Pridajte commit message (napr. "Add CI pipeline")
- Potvrdí sa commit do main vetvy a pipeline sa spustí

4.5. Sledovanie behu pipeline
- Pozorujte jednotlivé kroky v reálnom čase
- Skontrolujte logy jednotlivých taskov
- Overte, že všetky kroky prebehli úspešne (zelené fajky)
- Po dokončení testovania kliknite na "Tests" tab v pipeline run - mali by ste vidieť všetky test results
- Code coverage by mal byť tiež viditeľný v "Code Coverage" tab

**💡 Dôležitá poznámka o testoch:**
- Parameter `publishTestResults: true` v DotNetCoreCLI@2 task automaticky publikuje test results do Azure DevOps
- **NEPRIDÁVAJTE** samostatné tasky `PublishTestResults@2` alebo `PublishCodeCoverageResults@2` - to by spôsobilo duplicitu a warnings
- `--collect:"XPlat Code Coverage"` zbiera code coverage automaticky a publikuje ho
- Azure DevOps automaticky zobrazí test results a coverage v UI

5. Vytvorenie App Service v Azure (1B)

- V Azure Portal vytvorte App Service.
- Ak je dostupný Free (F1), môžete použiť. Ak nie, zvoľte B1.
- Vyberte runtime podľa typu aplikácie (.NET, Node, Python…).

**Postupné kroky pre študentov:**

5.1. Prihlásenie do Azure Portal
- Otvorte https://portal.azure.com
- Prihláste sa s vaším Azure účtom (Azure for Students alebo Free Trial)

5.2. Vytvorenie Resource Group (ak ešte nemáte)
- V hornom vyhľadávacom poli napíšte "Resource groups"
- Kliknite na "Create"
- Subscription: vyberte vašu subscription
- Resource group name: napr. "DevOps-RG" alebo "FMFIBlazor-RG"
- Region: vyberte najbližší región (napr. "West Europe" alebo "North Europe")
- Kliknite "Review + create" a potom "Create"

5.3. Vytvorenie App Service
- V hornom vyhľadávacom poli napíšte "App Services"
- Kliknite na "Create" → "Web App"

5.4. Konfigurácia Basics
- Subscription: vyberte vašu subscription
- Resource Group: vyberte resource group vytvorenú v kroku 5.2
- Name: zadajte jedinečný názov (napr. "fmfiblazor-app-123")
  - Tento názov bude súčasťou URL: `https://[name].azurewebsites.net`
- Publish: "Code"
- Runtime stack: vyberte podľa vašej aplikácie
  - Pre .NET/Blazor: ".NET 10" alebo ".NET 8"
  - Pre Node.js: "Node 18 LTS" alebo novší
- Operating System: "Linux" (lacnejší) alebo "Windows"
- Region: rovnaký ako resource group (West Europe)

5.5. Výber App Service Plan (Pricing)
- Kliknite na "Change size" pri App Service Plan
- Pre študentov:
  - Skúste najprv "F1" (Free) - ak je dostupný
  - Ak F1 nie je dostupný, použite "B1" (Basic) - pozor na kredity!
- Kliknite "Select"

5.6. Deployment (voliteľné v tomto kroku)
- Zatiaľ preskočte "Deployment" tab
- Deployment nastavíme neskôr cez Release Pipeline

5.7. Review + Create
- Skontrolujte všetky nastavenia
- Kliknite "Review + create"
- Skontrolujte cenu (ak používate B1)
- Kliknite "Create"
- Počkajte na dokončenie deployment (1-2 minúty)

5.8. Overenie App Service
- Po vytvorení kliknite na "Go to resource"
- V Overview by ste mali vidieť:
  - Default domain: `https://[your-app-name].azurewebsites.net`
- Kliknite na URL - mali by ste vidieť predvolenú Azure stránku
- Poznačte si názov App Service pre použitie v Release Pipeline (bod 6)

6. Release Pipeline (Classic) – 2025 poznámka (1B)

Aj keď je Release Pipeline zastaraná technológia, na účely cvičenia ju použijeme kvôli jednoduchosti.

- Pipeline nasadí build artefakt do App Service.
- Výber artefaktu: artefakt z predchádzajúcej CI pipeline.

**⚠️ POZNÁMKA - MODERNÁ ALTERNATÍVA (Pokročilí študenti):**

Namiesto kombinácie "CI YAML (bod 4) + Classic Release Pipeline (bod 6)" môžete použiť **Multi-Stage YAML Pipeline**, ktorá kombinuje build a deployment do jedného YAML súboru. Toto je **modernejší prístup** a je odporúčaný od 2022.

**Výhody multi-stage YAML:**
- ✅ Všetko v jednom súbore (infrastructure as code)
- ✅ Verzionované v Git (vidíte históriu zmien)
- ✅ Podporuje viacero prostredí (Dev, Test, Production)
- ✅ Approval gates cez Azure Environments
- ✅ Žiadna potreba Classic Release Pipeline UI

**Ak chcete použiť multi-stage YAML namiesto Classic Release Pipeline:**
- Namiesto jednoduchej CI pipeline (bod 4) vytvoríte multi-stage pipeline s Build a Deploy stages
- Vytvorte Azure Environments v Azure DevOps (Pipelines → Environments)
- Môžete pridať approval gates na Production environment

**📁 Referenčný súbor:** `azure-pipelines-simple.yml` v repozitári obsahuje kompletný funkčný príklad.

**Kompletný príklad multi-stage pipeline (Build + Deploy):**
```yaml
# Multi-stage pipeline - Build a Deployment

trigger:
  branches:
    include:
    - main

pool:
  vmImage: 'ubuntu-latest'

# ==============================================================================
# STAGE 1: BUILD
# ==============================================================================
stages:
- stage: Build
  displayName: 'Build and Test'
  jobs:
  - job: BuildJob
    displayName: 'Build Application'
    steps:

    - task: UseDotNet@2
      displayName: 'Inštalácia .NET SDK'
      inputs:
        packageType: 'sdk'
        version: '10.x'

    - task: DotNetCoreCLI@2
      displayName: 'Restore dependencies'
      inputs:
        command: 'restore'
        projects: '**/*.csproj'

    - task: DotNetCoreCLI@2
      displayName: 'Build project'
      inputs:
        command: 'build'
        projects: '**/*.csproj'
        arguments: '--configuration Release --no-restore'

    - task: DotNetCoreCLI@2
      displayName: 'Run tests'
      inputs:
        command: 'test'
        projects: '**/*Tests.csproj'
        arguments: '--configuration Release --no-build --collect:"XPlat Code Coverage"'
        publishTestResults: true

    - task: DotNetCoreCLI@2
      displayName: 'Publish application'
      inputs:
        command: 'publish'
        publishWebProjects: false
        projects: 'MyBlazorApp.csproj'
        arguments: '--configuration Release --output $(Build.ArtifactStagingDirectory) --no-build'
        zipAfterPublish: true

    - task: PublishBuildArtifacts@1
      displayName: 'Publish build artifacts'
      inputs:
        PathtoPublish: '$(Build.ArtifactStagingDirectory)'
        ArtifactName: 'drop'
        publishLocation: 'Container'

# ==============================================================================
# STAGE 2: DEPLOYMENT
# ==============================================================================
- stage: Deploy
  displayName: 'Deploy to Azure'
  dependsOn: Build
  condition: succeeded()
  jobs:
  - deployment: DeployToAzure
    displayName: 'Deploy to App Service'
    environment: 'Production'  # Vytvorte environment v Azure DevOps
    pool:
      vmImage: 'ubuntu-latest'
    strategy:
      runOnce:
        deploy:
          steps:

          - task: DownloadBuildArtifacts@1
            displayName: 'Download build artifacts'
            inputs:
              buildType: 'current'
              downloadType: 'single'
              artifactName: 'drop'
              downloadPath: '$(System.ArtifactsDirectory)'

          - task: AzureWebApp@1
            displayName: 'Deploy to Azure App Service'
            inputs:
              azureSubscription: 'your-service-connection'  # ZMEŇTE
              appType: 'webAppLinux'
              appName: 'your-app-service-name'  # ZMEŇTE
              package: '$(System.ArtifactsDirectory)/drop/*.zip'
              deploymentMethod: 'zipDeploy'
```

**Postupné kroky pre multi-stage YAML deployment:**

1. **Vytvorte Azure Environment**
   - V Azure DevOps prejdite do Pipelines → Environments
   - Kliknite "New environment"
   - Name: "Production" (alebo "Dev", podľa stage)
   - Resource: None (necháme prázdne)
   - Kliknite "Create"

2. **Nastavte Approval Gate (voliteľné)**
   - V environment "Production" kliknite na ⋮ → Approvals and checks
   - Kliknite "Approvals"
   - Vyberte approvers (aspoň 1 osoba)
   - Kliknite "Create"

3. **Vytvorte Service Connection pre Azure**
   - Prejdite do Project Settings → Service connections
   - Kliknite "New service connection"
   - Vyberte "Azure Resource Manager"
   - Authentication method: "Service principal (automatic)"
   - Scope level: Subscription
   - Vyberte vašu subscription a resource group
   - Service connection name: napr. "Azure-Connection"
   - Kliknite "Save"

4. **Upravte YAML súbor**
   - Skopírujte kompletný príklad vyššie do vášho `azure-pipelines.yml`
   - Zmeňte `azureSubscription` na názov vašej service connection
   - Zmeňte `appName` na názov vášho App Service
   - Zmeňte `projects: 'MyBlazorApp.csproj'` na váš projekt

5. **Commitnite a pushnite**
   - Uložte súbor a commitnite do main vetvy
   - Pipeline sa automaticky spustí
   - Build stage beží ako prvý
   - Deploy stage čaká na approval (ak ste nastavili)

6. **Schválenie deploymentu (ak máte approval gate)**
   - Po úspešnom builde sa deploy stage pozastaví
   - V Azure DevOps uvidíte notifikáciu "Waiting for approval"
   - Approver musí kliknúť na "Review" a "Approve"
   - Deployment sa automaticky spustí po schválení

**✅ Ak použijete multi-stage YAML, body 4 a 6 sú splnené jedným YAML súborom!**

**📁 Funkčný príklad:** Pozrite sa na `azure-pipelines-simple.yml` v repozitári.

---

**Pre študentov, ktorí chcú jednoduchší prístup s UI, pokračujte s Classic Release Pipeline nižšie:**

**Postupné kroky pre študentov:**

6.1. Vytvorenie Release Pipeline
- V Azure DevOps prejdite do Pipelines → Releases
- Kliknite na "New pipeline"
- Vyberte šablónu "Azure App Service deployment" alebo začnite s "Empty job"

6.2. Pridanie artefaktu
- Kliknite na "Add an artifact"
- Source type: "Build" (výber z predchádzajúcej CI pipeline)
- Project: vyberte váš projekt
- Source (build pipeline): vyberte vašu CI pipeline (tú, čo ste vytvorili v bode 4)
- Default version: "Latest"
- Source alias: _[názov-vašej-pipeline] (automaticky sa vyplní)
- Kliknite "Add"

6.3. Nastavenie Continuous Deployment triggeru
- Na pridanom artefakte kliknite na ikonu blesku (Lightning bolt)
- Zapnite "Continuous deployment trigger"
- Toto spôsobí, že po každom úspešnom CI builde sa automaticky spustí release

6.4. Pridanie Stage (prostredia)
- Kliknite na "Stage 1" alebo "Add a stage"
- Pomenujte ho napr. "DEV" alebo "Production"
- Kliknite na "1 job, 0 task" v stage

6.5. Konfigurácia Agent job
- Skontrolujte, že Agent pool je nastavený na "Azure Pipelines"
- Agent Specification: "windows-latest" alebo "ubuntu-latest"

6.6. Pridanie Deploy tasku
- Kliknite na "+" pri Agent job
- Vyhľadajte "Azure App Service deploy"
- Kliknite "Add"

6.7. Konfigurácia Azure App Service deploy tasku
- Display name: "Deploy to Azure App Service"
- Azure subscription: vyberte vašu Azure subscription (môže vyžadovať autorizáciu)
  - Ak sa objaví tlačidlo "Authorize", kliknite naň a prihláste sa do Azure
- App Service type: "Web App on Linux" alebo "Web App on Windows" (podľa vášho App Service)
- App Service name: vyberte názov vášho App Service vytvoreného v bode 5
- Package or folder: `$(System.DefaultWorkingDirectory)/_[názov-artefaktu]/drop/*.zip`
  - Môžete použiť browse button (...) na výber správneho artefaktu

6.8. Uloženie a vytvorenie release
- Kliknite "Save" v pravom hornom rohu
- Pridajte komentár (napr. "Initial release pipeline setup")
- Kliknite "OK"

6.9. Vytvorenie prvého release
- Kliknite "Create release" v pravom hornom rohu
- Skontrolujte verziu artefaktu
- Pridajte release description (voliteľné)
- Kliknite "Create"

6.10. Sledovanie deployment
- Kliknite na vytvorený release (napr. "Release-1")
- Pozorujte stage "DEV/Production"
- Kliknite na stage pre zobrazenie detailov
- Sledujte logy deployment procesu
- Po úspešnom deploymente by aplikácia mala bežať na vašom App Service URL

Cvičenie 2 – Práca s Git, PR, backlog (Aktualizované)
7. Backlog – Epics, Features, User Stories, Tasks (1B)

Vytvorte backlog pre svoj tím.

**Postupné kroky pre študentov:**

7.1. Navigácia do Boards
- V Azure DevOps prejdite do Boards → Backlogs
- Uistite sa, že ste v správnom projekte

7.2. Pochopenie hierarchie work items
- Epic: Najvyššia úroveň, väčší celok práce (napr. "Implementácia autentifikácie")
- Feature: Časť epicu, funkčná celost (napr. "Prihlásenie používateľov")
- User Story (alebo Product Backlog Item): Konkrétna funkcionalita z pohľadu používateľa
- Task: Technická úloha potrebná na dokončenie User Story

7.3. Vytvorenie Epic
- V ľavom menu pri "Backlogs" kliknite na dropdown
- Vyberte "Epics"
- Kliknite na "New Work Item"
- Title: napr. "DevOps Pipeline Setup"
- Description: popíšte účel (napr. "Nastavenie kompletného CI/CD procesu")
- Assigned To: priraďte člena tímu
- Area: vyberte area path (default je OK)
- Iteration: vyberte iteration/sprint
- Kliknite "Save & Close"

7.4. Vytvorenie Feature pod Epic
- Prepnite sa na "Features" v dropdown menu
- Kliknite na "New Work Item"
- Title: napr. "CI Pipeline Implementation"
- Parent: kliknite na "Add link" → "Parent" → vyberte vytvorený Epic
- Popíšte feature
- Kliknite "Save & Close"
- Vytvorte ďalšie features (napr. "CD Pipeline Implementation", "Branch Policies Setup")

7.5. Vytvorenie User Stories pod Feature
- Prepnite sa na "Backlog items" alebo "Stories"
- Kliknite na "New Work Item"
- Title: napr. "Create build pipeline with restore, build, test steps"
- Description: "As a developer, I want automated build pipeline so that code is validated automatically"
- Parent: priraďte k Feature "CI Pipeline Implementation"
- Acceptance Criteria: popíšte, čo musí byť splnené:
  ```
  - Pipeline má dotnet restore krok
  - Pipeline má dotnet build krok
  - Pipeline má dotnet test krok
  - Pipeline beží automaticky pri commit do main
  ```
- Story Points: odhadnite veľkosť (napr. 3, 5, 8)
- Priority: nastavte prioritu (1 = highest)
- Kliknite "Save & Close"

7.6. Vytvorenie Tasks pod User Story
- Otvorte vytvorenú User Story
- V spodnej časti kliknite na "Add link" → "New item" → "Task"
- Vytvorte jednotlivé tasky:
  - Task 1: "Setup YAML file structure"
  - Task 2: "Configure dotnet restore step"
  - Task 3: "Configure dotnet build step"
  - Task 4: "Configure dotnet test step"
  - Task 5: "Test pipeline execution"
- Pre každý task:
  - Estimated work: odhadnite hodiny (napr. 1h, 2h)
  - Assigned To: priraďte člena tímu
  - Activity: vyberte typ aktivity (Development, Testing, Documentation...)

7.7. Príklad kompletnej štruktúry backlógu
```
Epic: DevOps Pipeline Setup
├── Feature: CI Pipeline Implementation
│   ├── User Story: Create build pipeline
│   │   ├── Task: Setup YAML file structure
│   │   ├── Task: Configure build steps
│   │   └── Task: Test pipeline
│   └── User Story: Create PR validation pipeline
│       ├── Task: Create PR-triggered YAML
│       └── Task: Test with sample PR
├── Feature: CD Pipeline Implementation
│   └── User Story: Setup Release Pipeline to Azure
│       ├── Task: Create Release Pipeline
│       ├── Task: Configure Azure connection
│       └── Task: Test deployment
└── Feature: Branch Policies Setup
    └── User Story: Configure main branch protection
        ├── Task: Enable PR requirement
        ├── Task: Add build validation
        └── Task: Test policies with sample PR
```

7.8. Práca s Backlogom
- Board view: Boards → Boards (Kanban board - drag & drop)
- Sprint planning: Boards → Sprints (pridajte items do aktívneho šprintu)
- Queries: Boards → Queries (vytvorte vlastné dotazy na filtrovanie work items)

7.9. Best Practices
- Vždy priradite work item k PR (required v Branch Policies)
- Aktualizujte stav taskov (New → Active → Resolved → Closed)
- Používajte komentáre na komunikáciu v rámci work itemov
- Linkujte súvisiace work items (Related, Parent/Child, Predecessor/Successor)

8. Kontrolný build pre PR (1B)

Vytvorte druhú pipeline, ktorá bude:
- restore
- build
- test

Táto pipeline sa bude spúšťať len pri Pull Requestoch.

**⚠️ DÔLEŽITÉ:** Aj keď používate multi-stage YAML pipeline (bod 6 - moderná alternatíva), **PR validation pipeline je STÁLE samostatný súbor**! Nechcete, aby sa deployment spúšťal pri každom PR - len build a test validácia.

**Postupné kroky pre študentov:**

8.1. Vytvorenie novej pipeline pre PR validation
- V Azure DevOps prejdite do Pipelines → Create Pipeline
- Vyberte "Azure Repos Git"
- Vyberte váš repozitár

8.2. Výber šablóny a vytvorenie YAML
- Vyberte "Starter pipeline"
- Otvorí sa YAML editor

8.3. Úprava YAML súboru pre PR validation
- ODSTRÁŇTE trigger na main vetvu (nechceme, aby bežala pri každom commite)
- PRIDAJTE trigger len pre Pull Requesty
- Pridajte kroky: restore, build, test (БEZ publish - to nie je potrebné pre validáciu)

Príklad YAML:
```yaml
# Táto pipeline sa spúšťa LEN pri Pull Requestoch
trigger: none

pr:
  branches:
    include:
    - main

pool:
  vmImage: 'ubuntu-latest'

steps:
- task: UseDotNet@2
  displayName: 'Inštalácia .NET SDK'
  inputs:
    version: '10.x'

- task: DotNetCoreCLI@2
  displayName: 'Restore dependencies'
  inputs:
    command: 'restore'
    projects: '**/*.csproj'

- task: DotNetCoreCLI@2
  displayName: 'Build project'
  inputs:
    command: 'build'
    projects: '**/*.csproj'
    arguments: '--configuration Release --no-restore'

- task: DotNetCoreCLI@2
  displayName: 'Run tests'
  inputs:
    command: 'test'
    projects: '**/*Tests.csproj'
    arguments: '--configuration Release --no-build --collect:"XPlat Code Coverage"'
    publishTestResults: true  # Automaticky publikuje test results
```

8.4. Pomenovanie a uloženie pipeline
- Kliknite na "Save and run"
- Pomenujte súbor napr. `azure-pipelines-pr.yml`
- Commit message: "Add PR validation pipeline"
- Kliknite "Save and run"

8.5. Overenie funkčnosti (testovanie)
- Po vytvorení môžete otestovať tak, že:
  - Vytvoríte novú vetvu
  - Urobíte zmenu v kóde
  - Vytvoríte Pull Request do main
  - Pipeline by sa mala automaticky spustiť

8.6. Poznámky pre študentov
- Rozdiel medzi CI pipeline (bod 4) a PR pipeline:
  - CI pipeline: beží pri merge do main, vytvára artefakty, triggeruje deployment
  - PR pipeline: beží pri vytvorení/update PR, validuje kód pred mergom, NEVYTVÁRA artefakty
- Obe pipelines by mali mať podobnú logiku pre build a test, aby sa zabezpečilo, že kód funguje pred mergom

9. Branch Policies (1B)

Nastavte pravidlá:
- PR required
- Approval aspoň jedným členom tímu (Basic licencie sú prvé 5 zdarma)
- Povinné priradenie work itemu
- Spustenie kontrolného CI build-u

**Postupné kroky pre študentov:**

9.1. Navigácia do Branch Policies
- V Azure DevOps prejdite do Repos → Branches
- Nájdite svoju hlavnú vetvu (main)
- Kliknite na tri bodky (...) pri main vetve
- Vyberte "Branch policies"

9.2. Require a minimum number of reviewers
- V sekcii "Require a minimum number of reviewers" kliknite na prepínač
- Minimum number of reviewers: nastavte na "1"
- Zaškrtnite "Allow requestors to approve their own changes" (VYPNITE - requestor by nemal schvaľovať vlastný PR)
- Zaškrtnite "Prohibit the most recent pusher from approving their own changes" (ZAPNITE)
- Reset code reviewer votes: "When new changes are pushed" (ZAPNITE)

9.3. Check for linked work items
- Nájdite sekciu "Check for linked work items"
- Kliknite na prepínač
- Vyberte "Required" (PR nemôže byť dokončený bez work itemu)

9.4. Check for comment resolution
- Nájdite sekciu "Check for comment resolution"
- Kliknite na prepínač
- Vyberte "Required" (všetky komentáre musia byť vyriešené pred mergom)

9.5. Build Validation - pridanie PR pipeline
- Nájdite sekciu "Build Validation"
- Kliknite na "+" (Add build policy)
- Build pipeline: vyberte vašu PR validation pipeline (vytvorenú v bode 8)
- Display name: napr. "PR Validation Build"
- Trigger: "Automatic"
- Policy requirement: "Required" (PR nemôže byť dokončený bez úspešného buildu)
- Build expiration: "Immediately" alebo "After 12 hours"
- Kliknite "Save"

9.6. Overenie nastavení
- Skontrolujte všetky nastavené policies
- Mal by ste vidieť:
  - ✓ Require a minimum number of reviewers
  - ✓ Check for linked work items
  - ✓ Check for comment resolution
  - ✓ Build Validation (vaša PR pipeline)

9.7. Testovanie Branch Policies
- Vytvorte novú vetvu (napr. `feature/test-policies`)
- Urobte zmenu v kóde
- Commitnite a pushnite zmeny
- Vytvorte Pull Request do main
- Pokúste sa dokončiť PR - mali by ste vidieť required checks:
  - ⏳ Čaká na approval
  - ⏳ Čaká na linked work item
  - ⏳ Čaká na úspešný build
- Až po splnení všetkých podmienok bude možné dokončiť PR

10. Feature Branch + PR + Merge (1B)

- Vytvorte novú vetvu.
- Urobte zmenu v aplikácii (napr. text, názov endpointu).
- Vytvorte Pull Request.
- Priraďte work item.
- Nechajte druhého študenta spraviť review.
- Po schválení merge do hlavnej vetvy.

**Postupné kroky pre študentov:**

10.1. Vytvorenie Feature Branch (lokálne)
```bash
# Uistite sa, že ste na main vetve a máte najnovší kód
git checkout main
git pull origin main

# Vytvorte novú feature vetvu
git checkout -b feature/add-welcome-message

# Overenie, že ste na novej vetve
git branch
```

10.2. Vykonanie zmeny v kóde
- Otvorte projekt vo svojom IDE (VS Code, Visual Studio, atď.)
- Urobte zmenu v aplikácii, napríklad:
  - Zmeňte text na hlavnej stránke
  - Pridajte nový endpoint
  - Upravte CSS/styling
  - Pridajte nový komponent
- Príklad zmeny v Blazor aplikácii (Pages/Index.razor):
```razor
@page "/"

<PageTitle>Home</PageTitle>

<h1>Vitajte v DevOps cvičení!</h1>

<p>Toto je feature branch demo pre Pull Request.</p>
```

10.3. Commit a Push zmeny
```bash
# Pridajte zmenené súbory
git add .

# Commitnite zmeny s popisnou správou
git commit -m "Add welcome message to home page"

# Pushnite vetvu do remote repozitára
git push origin feature/add-welcome-message
```

10.4. Vytvorenie Pull Request v Azure DevOps
- V Azure DevOps prejdite do Repos → Pull Requests
- Kliknite na "New Pull Request"
- Alebo použite link, ktorý sa zobrazí po push v termináli
- Nastavte:
  - Source branch: `feature/add-welcome-message`
  - Target branch: `main`
- Vyplňte:
  - Title: "Add welcome message to home page"
  - Description: popíšte, čo ste zmenili a prečo
    ```
    ## Zmeny
    - Pridaný uvítací text na hlavnú stránku
    - Upravený dizajn homepage

    ## Test plan
    - [ ] Aplikácia sa builduje bez errorov
    - [ ] Testy prechádzajú
    - [ ] Homepage zobrazuje nový text
    ```

10.5. Priradenie Work Item k PR
- V pravej časti PR obrazovky nájdite "Work Items"
- Kliknite na "+"
- Vyhľadajte a vyberte relevantný work item (User Story alebo Task)
- Work item sa priradí k PR
- Toto je REQUIRED kvôli Branch Policies (bod 9)

10.6. Čakanie na Build Validation
- Po vytvorení PR sa automaticky spustí PR validation pipeline (bod 8)
- V PR by ste mali vidieť sekciu "Required" alebo "Policies"
- Sledujte status buildu:
  - ⏳ Queued
  - 🔄 Running
  - ✅ Succeeded alebo ❌ Failed
- Ak build failne, opravte chyby a pushnite nové commity

10.7. Reviewer Process (druhý študent)
- Druhý študent otvorí Pull Request
- Klikne na "Files" tab pre zobrazenie zmien
- Prezrie zmeny v kóde:
  - Skontroluje, či kód má zmysel
  - Hľadá potenciálne bugy alebo problémy
  - Overí, že zmeny zodpovedajú work itemu
- Pridá komentáre (voliteľné):
  - Kliknite na číslo riadku v zmenenom súbore
  - Napíšte komentár (napr. "Mohli by sme použiť lepší názov?")
  - Kliknite "Comment"
- Po review klikne na "Approve" (alebo "Reject" ak sú problémy)

10.8. Vyriešenie komentárov (ak existujú)
- Autor PR odpovie na komentáre
- Urobí potrebné úpravy v kóde
- Commitne a pushne zmeny:
```bash
git add .
git commit -m "Address review comments"
git push origin feature/add-welcome-message
```
- PR validation build sa spustí znova
- Reviewer označí komentáre ako "Resolved"

10.9. Kontrola splnenia všetkých policies
- Pred mergom overte, že všetky required policies sú splnené:
  - ✅ Work item linked
  - ✅ Build succeeded
  - ✅ Minimum reviewers (1 approval)
  - ✅ All comments resolved
- Tlačidlo "Complete" by malo byť aktívne (nie disabled)

10.10. Merge Pull Request
- Kliknite na "Complete" (zelené tlačidlo)
- V dialógovom okne nastavte:
  - Merge type: "Merge (no fast-forward)" (default)
  - ✅ Complete associated work items after merging
  - ✅ Delete `feature/add-welcome-message` after merging (cleanup)
  - Merge commit message: upravte ak chcete
- Kliknite "Complete merge"

10.11. Overenie úspešného merge
- PR by mal byť označený ako "Completed"
- Prejdite do Repos → Files → main branch
- Overte, že vaše zmeny sú v main vetve
- Prejdite do Pipelines:
  - CI pipeline (bod 4) by sa mala spustiť automaticky
  - Po úspešnom CI builde sa spustí Release Pipeline (bod 6)
  - Aplikácia by mala byť automaticky nasadená na Azure App Service

10.12. Lokálne cleanup (voliteľné)
```bash
# Prepnite sa späť na main
git checkout main

# Stiahnite najnovšie zmeny
git pull origin main

# Zmažte lokálnu feature branch
git branch -d feature/add-welcome-message
```

Odovzdanie zadania

Pridajte prístup pre: henrich.neuschl@gmail.com
- Typ: Basic (ak je k dispozícii), inak Stakeholder
- Ak pridanie cross-domain účtu vyžaduje povolenie, povoľte ho

Ak bude reviewer typ Stakeholder, nie je možné aprobovať PR.
Vtedy pošlite screenshoty vytvoreného a schváleného PR emailom na: Stefan.vodacek@softec.sk

---

## 📊 Stav Demo Projektu MyBlazorApp

Tento repozitár obsahuje demo projekt s nasledovným setup:

**✅ Implementované:**
- ✅ DevOps projekt a Git repozitár (bod 1)
- ✅ .NET 10.0 Blazor Server aplikácia s MySQL/EF Core (bod 2)
- ✅ Testovací projekt MyBlazorApp.Tests so skutočnými testami (bod 3)
  - UserTests, TransactionTests, TransactionTypeTests, UserFinancialTests
- ✅ **Dve verzie pipeline súborov:**
  - `azure-pipelines-simple.yml` - **jednoduchý multi-stage príklad** (ideálny na výuku)
    - Build stage (restore, build, test, publish, artifacts)
    - Deploy stage → FMFITEST
    - Minimálne variables, jasné komentáre v slovenčine
  - `azure-pipelines.yml` - **pokročilá multi-stage pipeline** s viacerými prostrediami (bod 4 + 6)
    - Build stage (restore, build, test, publish, artifacts)
    - Deploy_Dev stage → webapp-devopsfmfi2025 (automatický)
    - Deploy_Production stage → FMFITEST (s manual approval gate)
    - Obsahuje variables, pre-deployment checks, coverage reports
- ✅ Azure Environments (Dev, Production) s approval gates
- ✅ Deployment do dvoch App Services v Azure (bod 5)

**❌ Chýba dokončiť:**
- ⚠️ PR validation pipeline (bod 8) - vytvorte `azure-pipelines-pr.yml`
- ⚠️ Branch Policies nastavenie (bod 9) - pridajte PR validation build do policies
- ⚠️ Backlog štruktúra (bod 7) - vytvorte Epics, Features, User Stories, Tasks

**📝 Poznámky k demo projektu:**
- Projekt používa **pokročilý multi-stage YAML prístup** namiesto Classic Release Pipeline
- Toto je **modernejší** a odporúčaný spôsob od 2022
- Pre študentov je vhodné ukázať oba prístupy:
  1. Classic Release Pipeline (jednoduchší na pochopenie)
  2. Multi-stage YAML (production-ready prístup)

**Pre kompletné splnenie zadania zostáva:**
1. Vytvoriť `azure-pipelines-pr.yml` pre PR validation (bod 8)
2. Nastaviť Branch Policies s PR validation build (bod 9)
3. Vytvoriť backlog štruktúru v Boards (bod 7)
4. Otestovať celý PR workflow s work items (bod 10)