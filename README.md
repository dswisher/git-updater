# gup - multi-directory git status/fetch/rebase

This tool is intended to help manage projects that have a large number of related git repositories.
It will scan a directory hierarchy of git repos, and perform actions on each.
The currently supported actions are:

* git fetch
* status - an "improved" git status

Support is planned for a "merge" command, for repos that can be fast-forwarded.


# Installation

Clone the repo.
In the `src/GitUpdater` directory, do either:

    make install

or, if already installed:

    make update


## Libraries Used

* [Spectre.Console](https://github.com/spectreconsole/spectre.console)
* [System.IO.Abstractions](https://github.com/TestableIO/System.IO.Abstractions)
* [SimpleExec](https://github.com/adamralph/simple-exec)
* [Humanizer](https://github.com/Humanizr/Humanizer)


## Git

I originally tried using [libgit2sharp](https://github.com/libgit2/libgit2sharp), but it does not support SSL (git)
protocol, which is a requirement for my use case. It also complicated packaging, as it is a wrapper on top of a
native library, so building a true single-executable is not possible.  So, instead, I am using
[SimpleExec](https://github.com/adamralph/simple-exec) to run git commands, and then parsing the output.

The git documentation has been helpful, as it provides details on the output from each command.

* [git-fetch](https://git-scm.com/docs/git-fetch)
* [git-status](https://git-scm.com/docs/git-status)


## Similar Tools

* [Gita](https://github.com/nosarthur/gita)
* [gr](https://github.com/mixu/gr)
* [go-many-git](https://github.com/abrochard/go-many-git)
