# Introduction 
This Repo is used during our component detection/build task deployments to ensure that new updates do not cause a loss of components or a dramatic increase in time. 
With each new detector addition, this project might require updates to ensure that the tests pass during deployment. 

# Repo Structure
- Contracts
    - contract classes used during the testing phase, many of these are copies of ones found in our main repo, copied to not require taking the internal dependency
- Projects
    - reduced versions of popular open-source projects, keeping dependency lists but largely deleting source code. Sources are:
        - cgmanifest: A manifest file from running detection on office 
        - Maven: Apache Maven
        - NPM: express.js
        - pnpm: pnpm/pnpm github repo
        - corext: some office packages, 
        - nuget: a few nupkgs from VSO, and a project.assets created during buildtime
        - pip: vscode-python github repo
        - rust: uutils
        - yarn: react-sample-app
        - gradle: appcenter-test-espresso
- Tests
    - actual Test files.

# The build pipeline
The build pipeline runs deployed and PPE detection on 4 different platforms, then executes a dotnet test which compares the detection runs.

# Tests
At this point there are 4 tests:

    1. Compare metadata files, only thing that couldve changed without warning is a detector version being bumped.
    2. Compare manifest files, all compoonents should be present in both, and all their details should be identical
    3. Make sure the log file does not contain any '[ERROR]' tags
    4. Compare the detector run times and counts, will error if time increases drastically, the count decreases, or the count increases and there was no detector version update. 