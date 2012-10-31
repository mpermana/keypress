if "" == %mobs% set mobs="hornet|worm|vulture|eater|quadav|goblin|lizard|sapling|shell|worker|soldier|bunny"
mitaru -follow -loop -find %mobs% -exec "/a <t>" %homing%
