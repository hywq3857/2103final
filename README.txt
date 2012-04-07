Git User Guide for Dummies
===================
cd CS2103G30V01

git add *
git commit -m 'your mesage'
git pull origin master
--if there is no conflict
git push origin master
--if there are conflicts, solve the conflicts first, then
git commit -a (the effect of this line is equivalent to two lines: git add * and git commit -m 'your message')
git push origin master