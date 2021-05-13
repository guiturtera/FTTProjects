BRANCHES=$(git branch -r)
echo_red()
{
	echo -e "\e[1;31;49m$1\e[1;39;39m"
}

echo_blue()
{
	echo -e "\e[1;94;49m$1\e[1;39;39m"
}

echo_green()
{
	echo -e "\e[1;32;49m$1\e[1;39;49m"
}

create_worktree()
{
	REMOTE_BRANCH=$1
	LOCAL_BRANCH=${1:7}
	if (git worktree add --quiet -b $LOCAL_BRANCH ../$LOCAL_BRANCH $REMOTE_BRANCH); then
		echo_green "CREATED $LOCAL_BRANCH WORKTREE"
	else
		echo_red "FAILED TO CREATE $LOCAL_BRANCH WORKTREE"
		echo
	fi
}

echo
for i in $BRANCHES
do
	if [ $i != "->" -a $i != "origin/HEAD" ]; then
		if !(git show-ref --quiet refs/heads/${i:7}); then
			create_worktree $i
		else
			echo_blue "Worktree ${i:7} already exists. You may pull it."
		fi
	fi
done
read
#git worktree add -b ${i:7} ../${i:7} $i