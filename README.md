# WebVault

## Features
- Encrypt on client and upload encrypted text or files to online vault.
- Retrieve encrypted files from online vault.
- View both encrypted and decrypted files on the client.
- Allows you to store encrypted files in the vault, without also storing the key.

## Setup
A MSSQL is required to store data.
The connection string can be changed inside the appsettings.json file:
```
"ConnectionStrings": {
	"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=...;Trusted_Connection=True;MultipleActiveResultSets=true"
},
```

After the MSSQL is setup, you are ready to launch the server and client!

## Usage
1. Once you enter the site, you are required to login or register to use the vault.

2. After logging in, you can enter your vault and see your uploaded files.

3. Here you can view or download the encrypted files, or unlock(decrypt) them and see/download the decrypted file.
*Here you can also upload new files to the vault.*

## Packages
### Client
|Package Name|Version|
|----------------------|
|Microsoft.AspNetCore.Components.WebAssembly|7.0.14|
|Microsoft.AspNetCore.Components.WebAssembly.Authentication|7.0.14|
|Microsoft.AspNetCore.Components.WebAssembly.DevServer|7.0.14|
|Microsoft.Extensions.Http|7.0.0|
### Server
|Package Name|Version|
|----------------------|
|Microsoft.AspNetCore.ApiAuthorization.IdentityServer|7.0.14|
|Microsoft.AspNetCore.Components.WebAssembly.Server|7.0.14|
|Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore|7.0.14|
|Microsoft.AspNetCore.Identity.EntityFrameworkCore|7.0.14|
|Microsoft.AspNetCore.Identity.UI|7.0.14|
|Microsoft.EntityFrameworkCore.SqlServer|7.0.14|
|Microsoft.EntityFrameworkCore.Tools|7.0.14|

## Versions
### 1.0
Used WebVault project, which was Blazor server/client combined.
> [!WARNING]
> This introduced the problem, that uploading files, would still send them unecrypted to the server.
### 2.0
Split into Blazor WebAssembly and ASP .NET Core API.
This allows us to encrypt the files before they are sent to the server.
Although TLS would provide some security, sending the unecrypted files would still be unwise, since the server would the also have to encrypt them afterwards.
> [!IMPORTANT]
> Using WebAssembly to encrypt caused another issue. As of an earlier version of .NET, Crypto classes like Aes became unsupported, in favor of Mozilla's "Web Crypto API". It took some time to adjust the code to this, but it wasn't too difficult once the API documentation was looked through.
