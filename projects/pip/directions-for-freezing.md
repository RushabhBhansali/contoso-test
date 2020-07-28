You can't just run "pip freeze" from a normal terminal, this captures what's in your global environment instead of what's coming locally. 

Instead you want to: 
1. Create a new virtual env (see [here](https://packaging.python.org/guides/installing-using-pip-and-virtual-environments/))
1. Activate the virtual env
1. pip install -r requirements.txt
1. pip freeze

Pip freeze will write out to a console, so you can either pipe to a requirements.txt or copy paste.