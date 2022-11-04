@echo off
set python_dir=C:\Python39
set venv_project_path=DeepCTFenv

ECHO Installing...

if not exist "%python_dir%" (
	echo Installing python to %python_dir%
	.\python-3.9.1.exe /passive Include_test=0 TargetDir=%python_dir% /Include_debug=1
	echo done
)

if not exist "%python_dir%\Scripts\virtualenv.exe" (
	echo Installing virtualenv
	%python_dir%\Scripts\pip -q install virtualenv --disable-pip-version-check
)

if not exist "%venv_project_path%\Scripts\python.exe" (
	echo Creating python virtual environment
	%python_dir%\python -m venv %venv_project_path%
	
	echo Installing requirements
	%venv_project_path%\Scripts\pip install --disable-pip-version-check -q -r requirements.txt
)
echo on