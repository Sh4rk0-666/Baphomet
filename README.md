# Baphomet Ransomware

This is a proof of concept of how a ransomware works, and some techniques that we usually use to hijack our files.
This project is written in C# using the net-core application framework 3.1.The main idea of the code is to make it as readable as possible so that people have an idea of how this type of malware acts and works.

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


## twitter account: [@Chungo_0](https://twitter.com/Chungo_0)

### :warning: Warning!

***I Am Not Responsible of any Illegal Use***

