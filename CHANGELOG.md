# Changelog Norwish.Householder.Server
All notable changes to this project will be documented in this file.
This project adheres to [Semantic Versioning](http://semver.org/).

<<<<<<< HEAD
### Version 0.7.0, 2020-09-23
 - Refactored residents to users
 - Added endpoint for registering as a user
 - Added endpoint for logging in as a user
 - Added authorization to all endpoints except for user login and registration
 - Added support for JWTToken
 - Added endpoints for getting users

=======
>>>>>>> 7bae4360eec21ca3ed39c3db9ed982f25441a281
### Version 0.6.1, 2020-09-18
 - Refactored CQRS system to only use DTO's instead of common models
 - Added endpoint for getting reconciliations

### Version 0.6.0, 2020-09-18
 - Refactored entire project to be more structured and layered
 - Added endpoint for getting settlements
 - Added endpoint for creating new reconciliation

### Version 0.5.0, 2020-09-10
 - Added endpoints for posting and retrieving residents
 - Added endpoints for getting expenses per resident

### Version 0.4.1, 2020-08-19
 - Fixed sql script
 - Changed field in SettlementStatus.cs: Open => Pending

### Version 0.4.0, 2020-08-05
 - Added 'create_tables' sql script
 - Changed field in Reconciliation.cs: ActionDate => CreationDate

### Version 0.3.1, 2020-08-04
 - Added queries for Resident
 - Added commands for Resident

### Version 0.3.0, 2020-08-04
 - Added base queries and commands
 - Added LightInject dependency

### Version 0.2.0, 2020-08-04
Added contract models

### Version 0.1.0, 2020-08-04
Skeleton project
