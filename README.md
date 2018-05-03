## This is AES file encryptor decryptor

Written in c#. 

Multithreaded with progress bar. Async/await functionality.

It encrypts or decrypts everything in the selected folder. If there are other folders in the selected path this program will zip them together and then encrypt it.
After every encryption there will be a file generated to store your password's hash and encrypted files hashes.
If the file was changed after encryption, the program is designed to warn the user about it.