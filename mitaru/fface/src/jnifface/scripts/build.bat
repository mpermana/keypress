rem @echo off
set INCLUDE="c:\Program Files\java\jdk1.6.0_23\include";"C:\Program Files\java\jdk1.6.0_23\include\win32";"\Program Files\Microsoft Visual Studio 10.0\vc\include";"%windowssdkdir%\include"

set BUILDDIR=build\dll
set BUILDDIR=.
cl src\jnifface\cpp\jnifface.cpp src\jnifface\lib\fface.lib /Fe%BUILDDIR%\jnifface.dll /LDd
