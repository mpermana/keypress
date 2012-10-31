start mitaru -loop %name% -scan "appears extremely vulnerable to (.*)" -exec "5 EXTREME {1}" 
start mitaru -loop %name% -scan "appears highly vulnerable to (.*)" -exec "3 HIGHLY {1}" 
start mitaru -loop %name% -scan "appears vulnerable to (.*)" -exec "1 {1}" 

