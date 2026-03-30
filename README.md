# COM Hunter

**Administrator privileges required!**

COM Hunter (probably should have called it Hijack Hunter, but weh) is a command-line utility that can be used to sniff ETW for failed `RegistryOpen` events that reference COM paths. It leverages Event Trace for Windows (ETW) - which is the same mechanism used by ProcMon - to listen for and filter on specific events. 

There's been a lot of buzz around ETW lately, so I wanted to get in on the fun. Mainly about bypassing/evading ETW, so why don't we make it work for us? :p

This is very small codebase (.NET ftw?) but the compilation size is pretty big due to the need to include the C# ETW packages within it, #soz. If you are using regular build, its not configured to run anywhere else and build may fail depending on the target system.

In order to compile for execution on a different machine (see package note above) - you will need to use the `publish profile` set in `FolderProfile.pubxml`. 

> Hope this is useful :)
