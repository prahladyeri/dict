This dict program is the Windows equivalent of Linux dict command line utility.

**Genesis**

When I transitioned from Linux to Windows, a dictionary tool was very much needed and I was quite used to the Linux dict command.
For centuries, I scavenged the length and breadh of the Internet and tried various open source tools like WordWeb, Artha, etc. but none had the flexibility of running a simple command line utility like `dict epiphany`.

That is why I wrote the dict tool in .NET by utilizing the [WordNet open source database](http://wordnet.princeton.edu/). I hope this will be useful to you as well.

**Usage**

```bash
C:\programs>dict epiphany

Dict, version 1.0

Word: epiphany
Synset: epiphany
Gloss: a divine manifestation
Synset: Epiphany, Epiphany_of_Our_Lord, Twelfth_day, Three_Kings'_Day, January_6
Gloss: twelve days after Christmas; celebrates the visit of the three wise men to the infant Jesus
This software uses WordNetr lexical database (http://wordnet.princeton.edu/) by Princeton University.

Copyright (C) 2024 Prahlad Yeri <prahladyeri@yahoo.com>
License: MIT
This is free software; you are free to change and redistribute it.
There is NO WARRANTY, to the extent permitted by law.
```

**Build**

Any Visual Studio Edition including Professional, Community, Express, etc. released in the last decade can be used to build this program.

**Compatibility**

Runs on Windows 7 and later as long as .NET Framework 4 or above is installed. Should run on Linux too through the WINE compatibility layer.


**Credits**

Special thanks to the benevolent folks at Princeton University who decided to open source the wonderful [WordNet words database](https://wordnet.princeton.edu/).