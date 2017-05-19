# Bickern
A static file "server".

Will serve directories that has either of the following files
"index.html",
"index.htm",
"default.html",
"default.htm"

It will map it to a local "dns" of choice.

What is really going on is that it maps a proxy from 127.0.0.1:{available port} to a private address in the 127.0.0.0/8 space
and then adds an entry in the hostfile for that ip.

TODO:
- [ ] Add Config File
- [ ] Upload App
- [ ] Release Version 1.0
