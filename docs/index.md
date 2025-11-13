# Hearth
## Windows Software
### C# | Blazor | MAUI | SQLite

    Hearth is a Finance Management application. The app allows users to connect their personal bank accounts from
accross the internet, all the way from Discover Credit Cards to Venmo Debit Accounts. This functionality is
achieved through the integration of the Plaid API, which lets users easily and securely connect their bank
account in just a few clicks to my program. The Plaid API is also trusted and used by other software like 
Robinhood and RocketMoney. This Documentation is for my own personal use as I develop the app, aswell as to show
off to others who may be deeply interested and invested in the development of this proejct. 

Cheers!

---

## Design Philosohpy

    Hearth is designed fundamentally as a local desktop software application, and near everything about the
application's design serves this.
    - The app can function 100% offline, only needing internet if users wish to sync their data via Plaid.
    - All data (save for plaid) is stored locally on the client's device.
    - SQLite is used for simple data storage and manipulation.

## Pillars of Hearth

    There are Three Pillars of the projects that are vital for consistent and effecient development.
These pillars should be reviewed and in-mind before creating or editing anything in the repository.

### Object Models
[Models](models.md)

### Pages
[Pages}](pages.md)

### Components & CSS
[Components](components.md)