A not-really solver can be found in `src/`, along with a couple of simple tools.
They are written in NFlat, my own language, and make absolutely no sense unless
you read Japanese langauge.

The *real* source -- the source of my points -- can be found at [Google Drive]
(https://drive.google.com/drive/folders/0Bw_bA_jVTQrWcV9JRzB4ZGZybk0).

A related blog post is at http://d.hatena.ne.jp/yuizumi/20160808 (in Japanese).


## How to Build

On MacOS X with Mono 4.4.x:

~~~~
$ ( cd nflat ; xbuild )
$ ( cd packages ; nuget install )
$ make
~~~~


## About NFlat

NFlat is technically a [concatenative language][1], designed to run on [CLI][2]
(widely known as .NET Framework) and heavily influenced by [Mind][3] (a classic
Japanese-based language).

[1]: https://en.wikipedia.org/wiki/Concatenative_programming_language
[2]: https://en.wikipedia.org/wiki/Common_Language_Infrastructure
[3]: http://www.scripts-lab.co.jp/mind/whatsmind.html

`nflat/` contains the full source code for the compiler (NFlat compiled to C#).
It is fairly bleeding-edge and has plenty of bugs/issues.

The language spec is neither documented nor finalized.
