[![Build status](https://ci.appveyor.com/api/projects/status/a0vcwwelvrwsm998/branch/master?svg=true)](https://ci.appveyor.com/project/alexintime/bickern/branch/master)
[![FOSSA Status](https://app.fossa.io/api/projects/git%2Bgithub.com%2FVisualBean%2FBickern.svg?type=shield)](https://app.fossa.io/projects/git%2Bgithub.com%2FVisualBean%2FBickern?ref=badge_shield)

Very similar to Anvil for mac, but for Windows instead.

# Bickern
A static file "server".
It exists as a tray application:

![alt text](http://visualbean.io/wp-content/uploads/2017/07/bickern.png "bickern window")


---
HowTo:

1. Left click the anvil icon in your tray bar.
2. Press the plus icon.
3. Give it a name and find path to a folder you want to serve.

Et voila. either press the entry in the list or navigate with your browser to that url.

***

Will serve directories that has either of the following files
 + index.html
 + index.htm
 + default.html
 + default.htm

It will map it to a local "dns"-address of choice. I.E - mycatsite.dev

What is really going on is that it maps a proxy from 127.0.0.1:{available port} to a private address in the 127.0.0.0/8 space
and then adds an entry in the hostfile for that ip.
