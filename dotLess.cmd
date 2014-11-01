@echo off
cls

cd packages\dotless.1.4.1.0\tool\
dir/p

start

dotless.compiler.exe -w ../../../Cms/Content/bootstrap/bootstrap.less
