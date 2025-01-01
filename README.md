![license](https://img.shields.io/github/license/prahladyeri/dict.svg)
![last-commit](https://img.shields.io/github/last-commit/prahladyeri/dict.svg)
[![patreon](https://img.shields.io/badge/Patreon-brown.svg?logo=patreon)](https://www.patreon.com/prahladyeri)
[![paypal](https://img.shields.io/badge/PayPal-blue.svg?logo=paypal)](https://paypal.me/prahladyeri)
[![follow](https://img.shields.io/twitter/follow/prahladyeri.svg?style=social)](https://x.com/prahladyeri)

This dict program is the Windows equivalent of Linux `dict` command line utility.

**Genesis**

When I transitioned from Linux to Windows, a dictionary tool was very much needed and I was quite used to the Linux `dict` command.
For centuries, I scavenged the length and breadh of the Internet and tried various open source tools like WordWeb, Artha, etc. but none had the flexibility of running a simple command line utility like `dict epiphany`.

That is why I wrote the dict tool in .NET by utilizing the [WordNet open source database](http://wordnet.princeton.edu/). I hope this will be useful to you as well.

**Usage**

```bash
c:\programs>dict brevity
Dict, version 1.0
Word: brevity
Synset: brevity
Gloss: the use of brief expressions
Synset: brevity, briefness, transience
Gloss: the attribute of being brief or fleeting

This software uses WordNetr lexical database (http://wordnet.princeton.edu/) by Princeton University.
```

**Install**

Just get the latest release build [from here](https://github.com/prahladyeri/dict/releases/latest) and extract it inside a directory of your choice such as `c:\programs\`. Make sure it is included in the `%PATH%` environment variable as it's easier to run it from the command prompt.

**Build**

Any Visual Studio Edition including Professional, Community, Express, etc. released in the last decade can be used to build the solution.

**Compatibility**

Runs on Windows 7 and later as long as .NET Framework 4 or above is installed.
Should run on Linux too through WINE compatibility layer.

**Credits**

Special thanks to the benevolent folks at Princeton University who decided to open source the wonderful [WordNet words database](https://wordnet.princeton.edu/).