
#!/bin/bash 
startDir=$(pwd)
cd ./projects/symlinkHell
mkdir SymlinkedHell
levels=6 # how deep the tree goes
foldersPerLevel=4 

recursivelyCreateFolders () {
    dir=$1
    local currLevel=$2
    if [ "$currLevel" -gt "$levels" ]
    then
        return;
    fi
    cd "$dir"
    for i in $(seq 0 $foldersPerLevel) 
    do
        mkdir "dir$i" 
        # make a few cycles by symlinking up the filetree
        recursivelyCreateFolders "dir$i" $(($currLevel+1)) #create sub directories
    done;
    cd ../
}
getRandomDirectoryInHell() {
    min=$1
    max=$2
    directory="./SymlinkedHell/"
    depth=$(( $min + $(( $RANDOM % $(($max - $min)) )) ))
    for i in $(seq 0 $depth) 
    do
         directory=$directory"dir$(( $RANDOM % $foldersPerLevel))/";
    done
    directory=$directory"dir$(( $RANDOM % $foldersPerLevel))";
    echo "$directory"
}

makeSymlinks () {
    count=$1
    for i in $(seq 0 $count) 
    do
        from=$(getRandomDirectoryInHell 0 $levels)
        to=$(getRandomDirectoryInHell 0 $levels)
        sudo ln -s "$from/sym$i" "$to/" > /dev/null
    done
}

copyManifest () {
    count=$1
    for i in $(seq 0 $count) 
    do
        to=$(getRandomDirectoryInHell 0 $levels)
        cp "./cgmanifest.json" "$to"
    done;
}

echo "creating folder skeleton"
recursivelyCreateFolders "SymlinkedHell" 0
echo "creating symlinks"
makeSymlinks 50
copyManifest 100
echo $(pwd)
cd $startDir