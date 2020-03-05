# Baphomet Ransomware

This is a proof of concept of how a ransomware works, and some techniques that we usually use to hijack our files.

**Baphomet features**

* AES algorithm for file encryption.
* RSA encryption to encrypt key.
* Automatic propagation via USB.
* Hybrid encryption technique.
* Enumeration of processes to kill those selected.
* Internet connection test.
* victim information submissions (Public IP, Domainname, Country, OS.version, City, Machine name, etc).
* Program to decrypt the encryption key.
* Program to decrypt encrypted data.
* Hostname list to send the victim's data (redundancy).
* Doesn't detected to antivirus programs (Date: 3/5/2020 1:33pm).
* Hardcode image in base64 to change wallpaper (Baphomet image).

**Dynamic settings**

* List of directories we want to navigate.
* List of valid extensions.
* Host list to which we will send the data.
* List of processes that we want to stop in case they are running.
* Methods to convert base64 to image or download the image from a url.
* public key that will be hardcode to encrypt symmetric key.
