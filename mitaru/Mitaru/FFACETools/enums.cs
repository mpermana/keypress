﻿namespace FFACETools {
	#region Status

	/// <summary>
	/// Player Statuses
	/// </summary>
	public enum Status : byte {
		Standing = 0,
		Fighting = 1,
		Dead1 = 2,
		Dead2 = 3,
		Event = 4,
		Chocobo = 5,
		Healing = 33,
		FishBite = 38,
		Obtained = 39,
		RodBreak = 40,
		LineBreak = 41,
		LostCatch = 43,
		CatchMonster = 42,
		Synthing = 44,
		Sitting = 47,
		Fishing = 50

	} // @ public enum Status : byte

	/// <summary>
	/// Login Status
	/// </summary>
	public enum LoginStatus : byte {
		CharacterLoginScreen = 0,
		Loading = 1,
		LoggedIn = 2

	}

	#endregion

	#region Job

	/// <summary>
	/// Player Jobs
	/// </summary>
	public enum Job : byte {
		NONE = 0,
		WAR = 1,
		MNK = 2,
		WHM = 3,
		BLM = 4,
		RDM = 5,
		THF = 6,
		PLD = 7,
		DRK = 8,
		BST = 9,
		BRD = 10,
		RNG = 11,
		SAM = 12,
		NIN = 13,
		DRG = 14,
		SMN = 15,
		BLU = 16,
		COR = 17,
		PUP = 18,
		DNC = 19,
		SCH = 20

	} // @ public enum Job : byte

	#endregion

	#region StatusEffect

	/// <summary>
	/// Status effects
	/// </summary>
	public enum StatusEffect : short {
		Unknown = -1,
		KO = 0,
		Weakness = 1,
		Sleep = 2,
		Poison = 3,
		Paralysis = 4,
		Blindness = 5,
		Silence = 6,
		Petrification = 7,
		Disease = 8,
		Curse = 9,
		Stun = 10,
		Bind = 11,
		Weight = 12,
		Slow = 13,
		Charm1 = 14,
		Doom = 15,
		Amnesia = 16,
		Charm2 = 17,
		Gradual_Petrification = 18,
		Sleep2 = 19,
		Curse2 = 20,
		Addle = 21,
		Terror = 28,
		Mute = 29,
		Bane = 30,
		Plague = 31,
		Flee = 32,
		Haste = 33,
		Blaze_Spikes = 34,
		Ice_Spikes = 35,
		Blink = 36,
		Stoneskin = 37,
		Shock_Spikes = 38,
		Aquaveil = 39,
		Protect = 40,
		Shell = 41,
		Regen = 42,
		Refresh = 43,
		Mighty_Strikes = 44,
		Boost = 45,
		Hundred_Fists = 46,
		Manafont = 47,
		Chainspell = 48,
		Perfect_Dodge = 49,
		Invincible = 50,
		Blood_Weapon = 51,
		Soul_Voice = 52,
		Eagle_Eye_Shot = 53,
		Meikyo_Shisui = 54,
		Astral_Flow = 55,
		Berserk = 56,
		Defender = 57,
		Aggressor = 58,
		Focus = 59,
		Dodge = 60,
		Counterstance = 61,
		Sentinel = 62,
		Souleater = 63,
		Last_Resort = 64,
		Sneak_Attack = 65,
		//Copy_Image = 66,
		Utsusemi_1_Shadow_Left = 66,
		Third_Eye = 67,
		Warcry = 68,
		Invisible = 69,
		Deodorize = 70,
		Sneak = 71,
		Sharpshot = 72,
		Barrage = 73,
		Holy_Circle = 74,
		Arcane_Circle = 75,
		Hide = 76,
		Camouflage = 77,
		Divine_Seal = 78,
		Elemental_Seal = 79,
		STR_Boost1 = 80,
		DEX_Boost1 = 81,
		VIT_Boost1 = 82,
		AGI_Boost1 = 83,
		INT_Boost1 = 84,
		MND_Boost1 = 85,
		CHR_Boost1 = 86,
		Trick_Attack = 87,
		Max_HP_Boost = 88,
		Max_MP_Boost = 89,
		Accuracy_Boost = 90,
		Attack_Boost = 91,
		Evasion_Boost = 92,
		Defense_Boost = 93,
		Enfire = 94,
		Enblizzard = 95,
		Enaero = 96,
		Enstone = 97,
		Enthunder = 98,
		Enwater = 99,
		Barfire = 100,
		Barblizzard = 101,
		Baraero = 102,
		Barstone = 103,
		Barthunder = 104,
		Barwater = 105,
		Barsleep = 106,
		Barpoison = 107,
		Barparalyze = 108,
		Barblind = 109,
		Barsilence = 110,
		Barpetrify = 111,
		Barvirus = 112,
		Reraise = 113,
		Cover = 114,
		Unlimited_Shot = 115,
		Phalanx = 116,
		Warding_Circle = 117,
		Ancient_Circle = 118,
		STR_Boost2 = 119,
		DEX_Boost2 = 120,
		VIT_Boost2 = 121,
		AGI_Boost2 = 122,
		INT_Boost2 = 123,
		MND_Boost2 = 124,
		CHR_Boost2 = 125,
		Spirit_Surge = 126,
		Costume = 127,
		Burn = 128,
		Frost = 129,
		Choke = 130,
		Rasp = 131,
		Shock = 132,
		Drown = 133,
		Dia = 134,
		Bio = 135,
		STR_Down = 136,
		DEX_Down = 137,
		VIT_Down = 138,
		AGI_Down = 139,
		INT_Down = 140,
		MND_Down = 141,
		CHR_Down = 142,
		Level_Restriction = 143,
		Max_HP_Down = 144,
		Max_MP_Down = 145,
		Accuracy_Down = 146,
		Attack_Down = 147,
		Evasion_Down = 148,
		Defense_Down = 149,
		Physical_Shield = 150,
		Arrow_Shield = 151,
		Magic_Shield = 152,
		Damage_Spikes = 153,
		Shining_Ruby = 154,
		Medicine = 155,
		Flash = 156,
		Subjob_Restriction = 157,
		Provoke = 158,
		Penalty = 159,
		Preparations = 160,
		Sprint = 161,
		Enchantment = 162,
		Azure_Lore = 163,
		Chain_Affinity = 164,
		Burst_Affinity = 165,
		Overdrive = 166,
		Magic_Def_Down = 167,
		Inhibit_TP = 168,
		Potency = 169,
		Regain = 170,
		Pax = 171,
		Intension = 172,
		Dread_Spikes = 173,
		Magic_Acc_Down = 174,
		Magic_Atk_Down = 175,
		Quickening = 176,
		Encumbrance = 177,
		Firestorm = 178,
		Hailstorm = 179,
		Windstorm = 180,
		Sandstorm = 181,
		Thunderstorm = 182,
		Rainstorm = 183,
		Aurorastorm = 184,
		Voidstorm = 185,
		Helix = 186,
		Sublimation_Activated = 187,
		Sublimation_Complete = 188,
		Max_TP_Down = 189,
		Magic_Atk_Boost = 190,
		Magic_Def_Boost = 191,
		Requiem = 192,
		Lullaby = 193,
		Elegy = 194,
		Paeon = 195,
		Ballad = 196,
		Minne = 197,
		Minuet = 198,
		Madrigal = 199,
		Prelude = 200,
		Mambo = 201,
		Aubade = 202,
		Pastoral = 203,
		Hum = 204,
		Fantasia = 205,
		Operetta = 206,
		Capriccio = 207,
		Serenade = 208,
		Round = 209,
		Gavotte = 210,
		Fugue = 211,
		Rhapsody = 212,
		Aria = 213,
		March = 214,
		Etude = 215,
		Carol = 216,
		Threnody = 217,
		Hymnus = 218,
		Mazurka = 219,
		Sirvente = 220,
		Dirge = 221,
		Scherzo = 222,
		Auto_Regen = 233,
		Auto_Refresh = 234,
		Fishing_Imagery = 235,
		Woodworking_Imagery = 236,
		Smithing_Imagery = 237,
		Goldsmithing_Imagery = 238,
		Clothcraft_Imagery = 239,
		Leathercraft_Imagery = 240,
		Bonecraft_Imagery = 241,
		Alchemy_Imagery = 242,
		Cooking_Imagery = 243,
		Dedication = 249,
		Ef_Badge = 250,
		Food = 251,
		Chocobo = 252,
		Signet = 253,
		Battlefield = 254,
		Sanction = 256,
		Besieged = 257,
		Illusion = 258,
		//Encumbrance
		No_Weapons_Armor = 259,
		//Obliviscence
		No_Support_Job = 260,
		//Impairment
		No_Job_Abilities = 261,
		//Omerta
		No_Magic_Casting = 262,
		//Debilitation
		Penalty_to_Attributes = 263,
		Pathos = 264,
		Flurry = 265,
		Concentration = 266,
		Allied_Tags = 267,
		Sigil = 268,
		Level_Sync = 269,
		Aftermath_lvl1 = 270,
		Aftermath_lvl2 = 271,
		Aftermath_lvl3 = 272,
		Aftermath = 273,
		Enlight = 274,
		Auspice = 275,
		Confrontation = 276,
		Enfire_2 = 277,
		Enblizzard_2 = 278,
		Enaero_2 = 279,
		Enstone_2 = 280,
		Enthunder_2 = 281,
		Enwater_2 = 282,
		Perfect_Defense = 283,
		Egg = 284,
		Visitant = 285,
		Baramnesia = 286,
		Atma = 287,
		Endark = 288,
		Enmity_Boost = 289,
		Subtle_Blow_Plus = 290,
		Enmity_Down = 291,
		Pennant = 292,
		Negate_Petrify = 293,
		Negate_Terror = 294,
		Negate_Amnesia = 295,
		Negate_Doom = 296,
		Negate_Poison = 297,
		Critical_Hit_Evasion_Down = 298,
		Overload = 299,
		Fire_Maneuver = 300,
		Ice_Maneuver = 301,
		Wind_Maneuver = 302,
		Earth_Maneuver = 303,
		Thunder_Maneuver = 304,
		Water_Maneuver = 305,
		Light_Maneuver = 306,
		Dark_Maneuver = 307,
		DoubleUp_Chance = 308,
		Bust = 309,
		Fighters_Roll = 310,
		Monks_Roll = 311,
		Healers_Roll = 312,
		Wizards_Roll = 313,
		Warlocks_Roll = 314,
		Rogues_Roll = 315,
		Gallants_Roll = 316,
		Chaos_Roll = 317,
		Beast_Roll = 318,
		Choral_Roll = 319,
		Hunters_Roll = 320,
		Samurai_Roll = 321,
		Ninja_Roll = 322,
		Drachen_Roll = 323,
		Evokers_Roll = 324,
		Maguss_Roll = 325,
		Corsairs_Roll = 326,
		Puppet_Roll = 327,
		Dancers_Roll = 328,
		Scholars_Roll = 329,
		Bolters__Roll = 330,
		Casters_Roll = 331,
		Coursers_Roll = 332,
		Blitzers_Roll = 333,
		Tacticians_Roll = 334,
		Allies_Roll = 335,
		Misers_Roll = 336,
		Companions_Roll = 337,
		Avengers_Roll = 338,
		Warriors_Charge = 340,
		Formless_Strikes = 341,
		Assassins_Charge = 342,
		Feint = 343,
		Fealty = 344,
		Dark_Seal = 345,
		Diabolic_Eye = 346,
		Nightingale = 347,
		Troubadour = 348,
		Killer_Instinct = 349,
		Stealth_Shot = 350,
		Flashy_Shot = 351,
		Sange = 352,
		Hasso = 353,
		Seigan = 354,
		Convergence = 355,
		Diffusion = 356,
		Snake_Eye = 357,
		Light_Arts = 358,
		Dark_Arts = 359,
		Penury = 360,
		Parsimony = 361,
		Celerity = 362,
		Alacrity = 363,
		Rapture = 364,
		Ebullience = 365,
		Accession = 366,
		Manifestation = 367,
		Drain_Samba = 368,
		Aspir_Samba = 369,
		Haste_Samba = 370,
		Velocity_Shot = 371,
		Building_Flourish = 375,
		Trance = 376,
		Tabula_Rasa = 377,
		Drain_Daze = 378,
		Aspir_Daze = 379,
		Haste_Daze = 380,
		Finishing_Move1 = 381,
		Finishing_Move2 = 382,
		Finishing_Move3 = 383,
		Finishing_Move4 = 384,
		Finishing_Move5 = 385,
		Lethargic_Daze1 = 386,
		Lethargic_Daze2 = 387,
		Lethargic_Daze3 = 388,
		Lethargic_Daze4 = 389,
		Lethargic_Daze5 = 390,
		Sluggish_Daze1 = 391,
		Sluggish_Daze2 = 392,
		Sluggish_Daze3 = 393,
		Sluggish_Daze4 = 394,
		Sluggish_Daze5 = 395,
		Weakened_Daze1 = 396,
		Weakened_Daze2 = 397,
		Weakened_Daze3 = 398,
		Weakened_Daze4 = 399,
		Weakened_Daze5 = 400,
		Addendum_White = 401,
		Addendum_Black = 402,
		Reprisal = 403,
		Magic_Evasion_Down = 404,
		Retaliation = 405,
		Footwork = 406,
		Klimaform = 407,
		Sekkanoki = 408,
		Pianissimo = 409,
		Saber_Dance = 410,
		Fan_Dance = 411,
		Altruism = 412,
		Focalization = 413,
		Tranquility = 414,
		Equanimity = 415,
		Enlightenment = 416,
		Afflatus_Solace = 417,
		Afflatus_Misery = 418,
		Composure = 419,
		Yonin = 420,
		Innin = 421,
		Carbuncles_Favor = 422,
		Ifrits_Favor = 423,
		Shivas_Favor = 424,
		Garudas_Favor = 425,
		Titans_Favor = 426,
		Ramuhs_Favor = 427,
		Leviathans_Favor = 428,
		Fenrirs_Favor = 429,
		Diabolos_Favor = 430,
		Avatars_Favor = 431,
		Multi_Strikes = 432,
		Double_Shot = 433,
		Transcendency = 434,
		Restraint = 435,
		Perfect_Counter = 436,
		Mana_Wall = 437,
		Divine_Emblem = 438,
		Nether_Void = 439,
		Sengikori = 440,
		Futae = 441,
		Presto = 442,
		Climactic_Flourish = 443,
		Utsusemi_2_Shadows_Left = 444,
		Utsusemi_3_Shadows_Left = 445,
		Utsusemi_4_Shadows_Left = 446,
		Multi_Shots = 447,
		Bewildered_Daze1 = 448,
		Bewildered_Daze2 = 449,
		Bewildered_Daze3 = 450,
		Bewildered_Daze4 = 451,
		Bewildered_Daze5 = 452,
		Divine_Caress = 453,
		Saboteur = 454,
		Tenuto = 455,
		Spur = 456,
		Efflux = 457,
		Earthen_Armor = 458,
		Divine_Caress2 = 459,
		Blood_Rage = 460,
		Impetus = 461,
		Conspirator = 462,
		Sepulcher = 463,
		Arcane_Crest = 464,
		Hamanoha = 465,
		Dragon_Breaker = 466,
		Triple_Shot = 467,
		Striking_Flourish = 468,
		Perpetuance = 469,
		Immanence = 470,
		Migawari = 471,
		Muddled = 473,
		Prowess = 474,
		Voidwatcher = 475,
		Ensphere = 476,
		Sacrosanctity = 477,
		Palisade = 478,
		Scarlet_Delirium = 479,
		Scarlet_Delirium2 = 480,
		Decoy_Shot = 482,
		Hagakure = 483,
		Issekigan = 484,
		Unbridled_Learning = 485,
		Counter_Boost = 486,
		Endrain = 487,
		Enaspir = 488,
		Afterglow = 489

	} // @ public enum StatusEffect : short

	#endregion

	#region NavigatorTools Type-Enum

	/// <summary>
	/// HeadingType enum for determining return/set values for Navigator functions.
	/// </summary>
	public enum HeadingType : byte {
		Radians,
		Degrees
	}

	#endregion

	#region ChatMode

	/// <summary>
	/// Chat Modes
	/// </summary>
	public enum ChatMode : short {
		//------------------------------------------------------------------------------------------'
		//--------------------------------------Tekz eChatModes-------------------------------------'
		//------------------------------------------------------------------------------------------'
		Error = -1,				// "Invented" chat mode, to help catch errors
		Generic = 0,		// Unknown = 0,			 // Catch all. it's not a catch all.

		//--------------------------------------------------------------'
		//-Text That's Been Sent To The ChatLog By You aKa (The Player-)'
		//--------------------------------------------------------------'
		SentSay = 1,			 // = A Say Msg That The Used Sends 
		SentShout = 2,		   // = A Shout Msg That The Used Sends
		SentTell = 4,			// = my Tell to someone else
		SentParty = 5,		   // = my msg to Party
		SentLinkShell = 6,	   // = my msg to my linkshell
		SentEmote = 7,			  // = sent email

		//--------------------------------------------------------------'
		//----Text That's Been Recieved In ChatLog By Other Players-----'
		//--------------------------------------------------------------'
		RcvdSay = 9,			 // = A Say Msg That The Used Will See From Someone Else
		RcvdShout = 10,		  // = A Shout Msg That The Used Will See From Someone Else
		RcvdTell = 12,		   // = received a Tell
		RcvdParty = 13,		  // = received party text -- Thanks to AcidFang for this value (v1.5.4)
		RcvdLinkShell = 14,	  // = incoming linkshell text
		RcvdEmote = 15,			 // = received Emote

		//--------------------------------------------------------------'
		//-----------You aKa (The Player's) Fight Log Stuff-------------'
		//--------------------------------------------------------------'
		PlayerHits = 20,	// eg. Teknical hits the Thread Leech for 63 points of damage.
		PlayerMisses = 21,
		TargetUsesJobAbility = 22, // eg. The Thread Leech uses TP Drainkiss.
		SomeoneRecoversHP = 23,
		TargetHits = 28,	 // eg. The Thread Leech hits Teknical for 4 points of damage.
		TargetMisses = 29,	   // eg. The Thread Leech misses Teknical.
		PlayerAdditionalEffect = 30,
		PlayerRecoversHP = 31,		// Player casts Cure. Player recovers 30 HP.
		PlayerDefeats = 36,	  // eg. Teknical Defeats the River Crab. or whatever
		PlayedDefeated = 38,
		NPCHit = 40,
		NPCMiss = 41,
		NPCSpellEffect = 42,
		SomeoneSpellEffect = 43,
		SomeoneDefeats = 44,	 // = somebody "defeats the" river crab or whatever
		PlayerCastComplete = 50,
		PartySpellEffect = 51,
		PlayerStartCasting = 52, // eg. Teknical starts casting Dia on the Thread Leech., The Antican Princeps starts casting Flash.
		PlayerSpellResult = 56,
		PlayerRcvdEffect = 57, // The Antican Princeps casts Flash. <name> is blinded.
		PlayerSpellResist = 59,
		PlayerSpellEffect = 64,
		TargetEffectOff = 65,
		SomeoneNoEffect = 69,
		PlayerLearnedSpell = 81,
		Itemused = 90,
		SomeoneItemBadEffect = 91,
		SomeoneItemGoodEfect = 92,
		TargetActionStart = 100,
		PlayerUsesJobAbility = 101, // eg. Teknical uses Divine Seal.
		PlayerStatusResult = 102,
		TargetActionMiss = 104,
		PlayerReadiesMove = 110, // eg. The Thread Leech readies Brain Drain.
		SomeoneAbility = 111,
		SomeoneBadEffect = 112,
		PlayerWSMiss = 114,
		SynthResult = 121,	   // = you throw away a rusty subligar or whatever
		PlayersBadCast = 122,	// eg. Inturrupted or Unable to Cast. eg: Unable To Cast That Spell
		TellNotRcvd = 123,	   // = your tell was not received
		Obtained = 127,
		SkillBoost = 129,		// = you fishing skill rises 0.1 points
		Experience = 131,
		ActionStart = 135,
		LogoutMessage = 136,
		ItemSold = 138,		  // = item sold
		ClockInfo = 140,
		MoogleYellow = 141,
		NPCChat = 142,
		MoogleWhite = 144,
		FishObtained = 146,	  // "player caught ....!"
		FishResult = 148,		// = fishing result including: 
		NPCSpeaking = 152,	  // = something caught on hook... incorrect, NPC speaking to you
		CommandError = 157,	  // = A command error occurred
		DropRipCap = 159,		// = you release the ripped cap regretfully
		RegConquest = 161,	   // = regional conquest update message
		ChangeJob = 190,
		EffectWearOff = 191,	 // eg. Teknical's Protect effect wears off
		ServerNotice = 200,	   // = notice of upcoming server maintenance
		SearchComment = 204,
		LSMES = 205,
		Echo = 206,			  // = echo
		Examined = 208,
		AbilTimeLeft = 209    // Time left on "job ability"
		//--------------------------------------------------------------'
		//-----The Other Player aKa (The Target's) Fight Log Stuff------'
		//--------------------------------------------------------------'








		//-You didn't catch anything=,
		//-You lost your catch, 
		//-Whatever caught the hook was too small to catch, 
		//-Your line broke



	} // @ public enum ChatMode : short

	#endregion

	#region LineSettings

	/// <summary>
	/// LineSettings for GetNextLine
	/// </summary>
	[System.FlagsAttribute]
	public enum LineSettings : uint {
		/// <summary>
		/// Used if you want the Raw Text.
		/// </summary>
		RawText = 0,
		/// <summary>
		/// Cleans \x1E\x02 and \x1E\x01 for Items used in NPC Chat.
		/// </summary>
		CleanItemBytes = 1,
		/// <summary>
		/// Cleans \x1E\x03 and \x1E\x01 for Key Items used in NPC Chat.
		/// </summary>
		CleanKIBytes = 2,
		/// <summary>
		/// Cleans \x1E\x05 and \x1E\x01 for Objects mentioned in NPC Chat/Dialog. (Petra/Quarry/etc)
		/// </summary>
		CleanObjectBytes = 4,
		/// <summary>
		/// Cleans \xEF#### combinations for Elemental Icons (#### being \x1F through \x26)
		/// </summary>
		CleanElementIcons = 8,
		/// <summary>
		/// Cleans \xEF\x27 and \xEF\x28 (Open and Closing Brace)
		/// </summary>
		CleanATBrackets = 16,
		/// <summary>
		/// Cleans \r\n
		/// </summary>
		CleanNewLine = 32,
		/// <summary>
		/// Cleans uncategorized combinations (\x07, \x1E\xFC, \x1E\xFD, \x7F\x31, \x81\xA1, \x87\xB2, \x87\xB3, and \x1F\x## combinations)
		/// </summary>
		CleanOthers = 64,
		/// <summary>
		/// Cleans TimeStamp plugin's addition to the chatlog.
		/// </summary>
		CleanTimeStamp = 128,
		/// <summary>
		/// Puts items inside { and }. (Braces)  (overrides CleanItemBytes)
		/// </summary>
		ConvertItemBytes = 256,
		/// <summary>
		/// Puts Key Items inside [ and ]. (Brackets) (overrides CleanKIBytes)
		/// </summary>
		ConvertKIBytes = 512,
		/// <summary>
		/// Puts Objects of interest inside ( and ). (Parenthesis) (overrides CleanObjectBytes)
		/// </summary>
		ConvertObjectBytes = 1024,
		/// <summary>
		/// Replaces the 8 elements with printable 3-character combinations. &lt; FIAETWLD &gt;   (overrides CleanElementIcons)
		/// </summary>
		ConvertElementIcons = 2048,
		/// <summary>
		/// Replaces Auto-Translate Brackets with &lt;{ and }&gt;  (overrides CleanATBrackets)
		/// </summary>
		ConvertATBrackets = 4096,
		/// <summary>
		/// Cleans everything INCLUDING new line, no conversion.
		/// </summary>
		CleanAll = CleanOthers | CleanNewLine | CleanItemBytes | CleanKIBytes | CleanObjectBytes | CleanATBrackets | CleanElementIcons | CleanTimeStamp,
		/// <summary>
		/// Cleans everything EXCEPT new line, no conversion.
		/// </summary>
		CleanAllKeepNewLine = CleanOthers | CleanItemBytes | CleanKIBytes | CleanObjectBytes | CleanATBrackets | CleanElementIcons | CleanTimeStamp,
		/// <summary>
		/// Converts everything that can be converted. (Please don't use this by itself.)
		/// </summary>
		ConvertAll = ConvertATBrackets | ConvertElementIcons | ConvertItemBytes | ConvertKIBytes | ConvertObjectBytes,
		/// <summary>
		/// Converts everything that can be converted, cleans everything else INCLUDING new line.
		/// </summary>
		CleanAndConvertAll = ConvertAll | CleanNewLine | CleanOthers | CleanTimeStamp,
		/// <summary>
		/// Converts everything that can be converted, cleans everything else EXCEPT new line.
		/// </summary>
		KeepNewLine = ConvertAll | CleanOthers | CleanTimeStamp,
		/// <summary>
		/// Converts everything that can be converted, cleans everything else EXCEPT new line (same as KeepNewLine).
		/// </summary>
		CleanAndConvertAllKeepNewLine = ConvertAll | CleanOthers | CleanTimeStamp,
		/// <summary>
		/// Yekyaa's personal favorite.  Converts AT Brackets, Element Icons, cleans everything else INCLUDING new line.
		/// </summary>
		OldSchool = CleanTimeStamp | CleanOthers | CleanNewLine | CleanItemBytes | CleanKIBytes | CleanObjectBytes | ConvertATBrackets | ConvertElementIcons,
		/// <summary>
		/// Converts AT Brackets, Element Icons, cleans everything else EXCEPT new line.
		/// </summary>
		ReallyOldSchool = CleanTimeStamp | CleanOthers | CleanItemBytes | CleanKIBytes | CleanObjectBytes | ConvertATBrackets | ConvertElementIcons,
		/// <summary>
		/// Default used when getting DialogText.Question and Options (ConvertAll|CleanOthers|CleanNewLine)
		/// </summary>
		DialogDefault = ConvertAll | CleanOthers | CleanNewLine

	};

	#endregion

	#region Zone

	/// <summary>
	/// Zones
	/// </summary>
	public enum Zone : short {
		Unknown = 0,
		Phanauet_Channel = 1,
		Carpenters_Landing = 2,
		Manaclipper = 3,
		Bibiki_Bay = 4,
		Uleguerand_Range = 5,
		Bearclaw_Pinnacle = 6,
		Attohwa_Chasm = 7,
		Boneyard_Gully = 8,
		PsoXja = 9,
		The_Shrouded_Maw = 10,
		Oldton_Movalpolos = 11,
		Newton_Movalpolos = 12,
		Mineshaft_2716 = 13,
		Hall_of_Transference = 14,
		Abyssea__Konschtat = 15,
		Promyvion_Holla = 16,
		Spire_of_Holla = 17,
		Promyvion_Dem = 18,
		Spire_of_Dem = 19,
		Promyvion_Mea = 20,
		Spire_of_Mea = 21,
		Promyvion_Vahzl = 22,
		Spire_of_Vahzl = 23,
		Lufaise_Meadows = 24,
		Misareaux_Coast = 25,
		Tavnazian_Safehold = 26,
		Phomiuna_Aqueducts = 27,
		Sacrarium = 28,
		Riverne__Site_B01 = 29,
		Riverne__Site_A01 = 30,
		Monarch_Linn = 31,
		Sealions_Den = 32,
		AlTaieu = 33,
		Grand_Palace_of_HuXzoi = 34,
		The_Garden_of_RuHmet = 35,
		Empyreal_Paradox = 36,
		Temenos = 37,
		Apollyon = 38,
		Dynamis_Valkurm = 39,
		Dynamis_Buberimu = 40,
		Dynamis_Qufim = 41,
		Dynamis_Tavnazia = 42,
		Diorama_Abdhaljs_Ghelsba = 43,
		Abdhaljs_Isle_Purgonorgo = 44,
		Abyssea__Tahrongi = 45,
		Open_sea_route_to_Al_Zahbi = 46,
		Open_sea_route_to_Mhaura = 47,
		Al_Zahbi = 48,
		Aht_Urgan_Whitegate = 50,
		Wajaom_Woodlands = 51,
		Bhaflau_Thickets = 52,
		Nashmau = 53,
		Arrapago_Reef = 54,
		Ilrusi_Atoll = 55,
		Periqia = 56,
		Talacca_Cove = 57,
		Silver_Sea_Route_to_Nashmau = 58,
		Silver_Sea_Route_to_Al_Zahbi = 59,
		The_Ashu_Talif = 60,
		Mount_Zhayolm = 61,
		Halvung = 62,
		Lebros_Cavern = 63,
		Navukgo_Execution_Chamber = 64,
		Mamook = 65,
		Mamool_Ja_Training_Grounds = 66,
		Jade_Sepulcher = 67,
		Aydeewa_Subterrane = 68,
		Leujaoam_Sanctum = 69,
		Chocobo_Circuit = 70,
		The_Colosseum = 71,
		Alzadaal_Undersea_Ruins = 72,
		Zhayolm_Remnants = 73,
		Arrapago_Remnants = 74,
		Bhaflau_Remnants = 75,
		Silver_Sea_Remnants = 76,
		Nyzul_Isle = 77,
		Hazhalm_Testing_Grounds = 78,
		Caedarva_Mire = 79,
		Southern_San_DOria_S = 80,
		East_Ronfaure_S = 81,
		Jugner_Forest_S = 82,
		Vunkerl_Inlet_S = 83,
		Batallia_Downs_S = 84,
		La_Vaule_S = 85,
		Everbloom_Hollow = 86,
		Bastok_Markets_S = 87,
		North_Gustaberg_S = 88,
		Grauberg_S = 89,
		Pashhow_Marshlands_S = 90,
		Rolanberry_Fields_S = 91,
		Beadeaux_S = 92,
		Ruhotz_Silvermines = 93,
		Windurst_Waters_S = 94,
		West_Sarutabaruta_S = 95,
		Fort_Karugo_Narugo_S = 96,
		Meriphataud_Mountains_S = 97,
		Sauromugue_Champaign_S = 98,
		Castle_Oztroja_S = 99,
		Ronfaure_West = 100,
		Ronfaure_East = 101,
		La_Theine_Plateau = 102,
		Valkurm_Dunes = 103,
		Jugner_Forest = 104,
		Batallia_Downs = 105,
		Gustaberg_North = 106,
		Gustaberg_South = 107,
		Konschtat_Highlands = 108,
		Pashhow_Marshlands = 109,
		Rolanberry_Fields = 110,
		Beaucedine_Glacier = 111,
		Xarcabard = 112,
		Cape_Terriggan = 113,
		Altepa_Eastern_Desert = 114,
		Sarutabaruta_West = 115,
		Sarutabaruta_East = 116,
		Tahrongi_Canyon = 117,
		Buburimu_Peninsula = 118,
		Meriphataud_Mountains = 119,
		Sauromugue_Champaign = 120,
		Sanctuary_of_ZiTah = 121,
		RoMaeve = 122,
		Yuhtunga_Jungle = 123,
		Yhoator_Jungle = 124,
		Altepa_Western_Desert = 125,
		Qufim_Island = 126,
		Behemoths_Dominion = 127,
		Valley_of_Sorrows = 128,
		Ghoyus_Reverie = 129,
		RuAun_Gardens = 130,
		Mordion_Gaol = 131,
		Abyssea__La_Theine = 132,
		Lobby = 133,
		Dynamis_Beaucedine = 134,
		Dynamis_Xarcabard = 135,
		Beaucedine_Glacier_S = 136,
		Xarcabard_S = 137,
		Castle_Zvahl_Baileys_S = 138,
		Horlais_Peak = 139,
		Ghelsba_Outpost = 140,
		Fort_Ghelsba = 141,
		Yughott_Grotto = 142,
		Palborough_Mines = 143,
		Waughroon_Shrine = 144,
		Giddeus = 145,
		Balgas_Dais = 146,
		Beadeaux = 147,
		Qulun_Dome = 148,
		Davoi = 149,
		Monastic_Cavern = 150,
		Castle_Oztroja = 151,
		Altar_Room = 152,
		Boyahda_Tree = 153,
		Dragons_Aery = 154,
		Castle_Zvahl_Keep_S = 155,
		Throne_Room_S = 156,
		Delkfutts_Middle_Tower = 157,
		Delkfutts_Upper_Tower = 158,
		Temple_of_Uggalepih = 159,
		Den_of_Rancor = 160,
		Castle_Zvahl_Baileys = 161,
		Castle_Zvahl_Keep = 162,
		Sacrificial_chamber = 163,
		Garlaige_Citadel_S = 164,
		Throne_Room = 165,
		Ranguemont_Pass = 166,
		Bostaunieux_Oubliette = 167,
		Chamber_of_Oracles = 168,
		Toraimarai_Canal = 169,
		Full_Moon_Fountain = 170,
		Crawlers_Nest_S = 171,
		Zeruhn_Mines = 172,
		Korroloka_Tunnel = 173,
		Kuftal_Tunnel = 174,
		The_Eldieme_Necropolis_S = 175,
		Sea_Serpent_Grotto = 176,
		VeLugannon_Palace = 177,
		Shrine_of_RuAvitau = 178,
		Stellar_Fulcrum = 179,
		LaLoff_Amphitheatre = 180,
		The_Celestial_Nexus = 181,
		Walk_of_Echoes = 182,
		The_Last_Stand = 183,
		Delkfutts_Lower_Tower = 184,
		Dynamis_San_DOria = 185,
		Dynamis_Bastok = 186,
		Dynamis_Windurst = 187,
		Dynamis_Jeuno = 188,
		King_Ranperres_Tomb = 190,
		Dangruf_Wadi = 191,
		Horutoto_Inner_Ruins = 192,
		Ordelles_Caves = 193,
		Outer_Horutoto_Ruins = 194,
		Eldieme_Necropolis = 195,
		Gusgen_Mines = 196,
		Crawlers_Nest = 197,
		Maze_of_Shakhrami = 198,
		Garlaige_Citadel = 200,
		Cloister_of_Gales = 201,
		Cloister_of_Storms = 202,
		Cloister_of_Frost = 203,
		FeiYin = 204,
		Ifrits_Cauldron = 205,
		QuBia_Arena = 206,
		Cloister_of_Flames = 207,
		Quicksand_Caves = 208,
		Cloister_of_Tremors = 209,
		Cloister_of_Tides = 211,
		Gustav_Tunnel = 212,
		Labyrinth_of_Onzozo = 213,
		Abyssea__Attohwa = 215,
		Abyssea__Misareaux = 216,
		Abyssea__Vunkerl = 217,
		Abyssea__Altepa = 218,
		Ferry_between_Mhaura__Selbina = 220,
		Ferry_between_Selbina__Mhaura = 221,
		Airship_from_San_DOria_to_Jeuno = 223,
		Airship_from_Bastok_to_Jeuno = 224,
		Airship_from_Windurst_to_Jeuno = 225,
		Airship_from_Kazham_to_Jeuno = 226,
		Ferry_between_Mhaura__Selbina_Pirates = 227,
		Ferry_between_Selbina__Mhaura_Pirates = 228,
		Southern_San_DOria = 230,
		Northern_San_DOria = 231,
		Port_San_DOria = 232,
		Chateau_DOraguille = 233,
		Bastok_Mines = 234,
		Bastok_Markets = 235,
		Port_Bastok = 236,
		Metalworks = 237,
		Windurst_Waters = 238,
		Windurst_Walls = 239,
		Port_Windurst = 240,
		Windurst_Woods = 241,
		Heavens_Tower = 242,
		Ru_Lude_Gardens = 243,
		Upper_Jeuno = 244,
		Lower_Jeuno = 245,
		Port_Jeuno = 246,
		Rabao = 247,
		Selbina = 248,
		Mhaura = 249,
		Kazham = 250,
		Hall_of_the_Gods = 251,
		Norg = 252,
		Abyssea__Uleguerand = 253,
		Abyssea__Grauberg = 254,
		Abyssea__Empyreal_Paradox = 255

	} // @ public enum Zone : short 

	#endregion

	#region SpellList

	/// <summary>
	/// Spell List
	/// </summary>
	public enum SpellList : short {
		Unknown = 0,
		Cure = 2,
		Cure_II = 4,
		Cure_III = 6,
		Cure_IV = 8,
		Cure_V = 10,
		Cure_VI = 12,
		Curaga = 14,
		Curaga_II = 16,
		Curaga_III = 18,
		Curaga_IV = 20,
		Curaga_V = 22,
		Raise = 24,
		Raise_II = 26,
		Poisona = 28,
		Paralyna = 30,
		Blindna = 32,
		Silena = 34,
		Stona = 36,
		Viruna = 38,
		Cursna = 40,
		Holy = 42,
		Holy_II = 44,
		Dia = 46,
		Dia_II = 48,
		Dia_III = 50,
		Dia_IV = 52,
		Dia_V = 54,
		Banish = 56,
		Banish_II = 58,
		Banish_III = 60,
		Banish_IV = 62,
		Banish_V = 64,
		Diaga = 66,
		Diaga_II = 68,
		Diaga_III = 70,
		Diaga_IV = 72,
		Diaga_V = 74,
		Banishga = 76,
		Banishga_II = 78,
		Banishga_III = 80,
		Banishga_IV = 82,
		Banishga_V = 84,
		Protect = 86,
		Protect_II = 88,
		Protect_III = 90,
		Protect_IV = 92,
		Protect_V = 94,
		Shell = 96,
		Shell_II = 98,
		Shell_III = 100,
		Shell_IV = 102,
		Shell_V = 104,
		Blink = 106,
		Stoneskin = 108,
		Aquaveil = 110,
		Slow = 112,
		Haste = 114,
		Paralyze = 116,
		Silence = 118,
		Barfire = 120,
		Barblizzard = 122,
		Baraero = 124,
		Barstone = 126,
		Barthunder = 128,
		Barwater = 130,
		Barfira = 132,
		Barblizzara = 134,
		Baraera = 136,
		Barstonra = 138,
		Barthundra = 140,
		Barwatera = 142,
		Barsleep = 144,
		Barpoison = 146,
		Barparalyze = 148,
		Barblind = 150,
		Barsilence = 152,
		Barpetrify = 154,
		Barvirus = 156,
		Slow_II = 158,
		Paralyze_II = 160,
		Recall_Jugner = 162,
		Recall_Pashh = 164,
		Recall_Meriph = 166,
		Baramnesia = 168,
		Baramnesra = 170,
		Barsleepra = 172,
		Barpoisonra = 174,
		Barparalyzra = 176,
		Barblindra = 178,
		Barsilencera = 180,
		Barpetra = 182,
		Barvira = 184,
		Cura = 186,
		Sacrifice = 188,
		Esuna = 190,
		Auspice = 192,
		Reprisal = 194,
		Repose = 196,
		Sandstorm = 198,
		Enfire = 200,
		Enblizzard = 202,
		Enaero = 204,
		Enstone = 206,
		Enthunder = 208,
		Enwater = 210,
		Phalanx = 212,
		Phalanx_II = 214,
		Regen = 216,
		Refresh = 218,
		Regen_II = 220,
		Regen_III = 222,
		Flash = 224,
		Rainstorm = 226,
		Windstorm = 228,
		Firestorm = 230,
		Hailstorm = 232,
		Thunderstorm = 234,
		Voidstorm = 236,
		Aurorastorm = 238,
		Teleport_Yhoat = 240,
		Teleport_Altep = 242,
		Teleport_Holla = 244,
		Teleport_Dem = 246,
		Teleport_Mea = 248,
		Protectra = 250,
		Protectra_II = 252,
		Protectra_III = 254,
		Protectra_IV = 256,
		Protectra_V = 258,
		Shellra = 260,
		Shellra_II = 262,
		Shellra_III = 264,
		Shellra_IV = 266,
		Shellra_V = 268,
		Reraise = 270,
		Invisible = 272,
		Sneak = 274,
		Deodorize = 276,
		Teleport_Vahzl = 278,
		Raise_III = 280,
		Reraise_II = 282,
		Reraise_III = 284,
		Erase = 286,
		Fire = 288,
		Fire_II = 290,
		Fire_III = 292,
		Fire_IV = 294,
		Fire_V = 296,
		Blizzard = 298,
		Blizzard_II = 300,
		Blizzard_III = 302,
		Blizzard_IV = 304,
		Blizzard_V = 306,
		Aero = 308,
		Aero_II = 310,
		Aero_III = 312,
		Aero_IV = 314,
		Aero_V = 316,
		Stone = 318,
		Stone_II = 320,
		Stone_III = 322,
		Stone_IV = 324,
		Stone_V = 326,
		Thunder = 328,
		Thunder_II = 330,
		Thunder_III = 332,
		Thunder_IV = 334,
		Thunder_V = 336,
		Water = 338,
		Water_II = 340,
		Water_III = 342,
		Water_IV = 344,
		Water_V = 346,
		Firaga = 348,
		Firaga_II = 350,
		Firaga_III = 352,
		Firaga_IV = 354,
		Firaga_V = 356,
		Blizzaga = 358,
		Blizzaga_II = 360,
		Blizzaga_III = 362,
		Blizzaga_IV = 364,
		Blizzaga_V = 366,
		Aeroga = 368,
		Aeroga_II = 370,
		Aeroga_III = 372,
		Aeroga_IV = 374,
		Aeroga_V = 376,
		Stonega = 378,
		Stonega_II = 380,
		Stonega_III = 382,
		Stonega_IV = 384,
		Stonega_V = 386,
		Thundaga = 388,
		Thundaga_II = 390,
		Thundaga_III = 392,
		Thundaga_IV = 394,
		Thundaga_V = 396,
		Waterga = 398,
		Waterga_II = 400,
		Waterga_III = 402,
		Waterga_IV = 404,
		Waterga_V = 406,
		Flare = 408,
		Flare_II = 410,
		Freeze = 412,
		Freeze_II = 414,
		Tornado = 416,
		Tornado_II = 418,
		Quake = 420,
		Quake_II = 422,
		Burst = 424,
		Burst_II = 426,
		Flood = 428,
		Flood_II = 430,
		Gravity = 432,
		Gravity_II = 434,
		Meteor = 436,
		Meteor_II = 438,
		Poison = 440,
		Poison_II = 442,
		Poison_III = 444,
		Poison_IV = 446,
		Poison_V = 448,
		Poisonga = 450,
		Poisonga_II = 452,
		Poisonga_III = 454,
		Poisonga_IV = 456,
		Poisonga_V = 458,
		Bio = 460,
		Bio_II = 462,
		Bio_III = 464,
		Bio_IV = 466,
		Bio_V = 468,
		Burn = 470,
		Frost = 472,
		Choke = 474,
		Rasp = 476,
		Shock = 478,
		Drown = 480,
		Retrace = 482,
		Absorb_ACC = 484,
		Drain = 490,
		Drain_II = 492,
		Aspir = 494,
		Aspir_II = 496,
		Blaze_Spikes = 498,
		Ice_Spikes = 500,
		Shock_Spikes = 502,
		Stun = 504,
		Sleep = 506,
		Blind = 508,
		Break = 510,
		Virus = 512,
		Curse = 514,
		Bind = 516,
		Sleep_II = 518,
		Dispel = 520,
		Warp = 522,
		Warp_II = 524,
		Escape = 526,
		Tractor = 528,
		Tractor_II = 530,
		Absorb_STR = 532,
		Absorb_DEX = 534,
		Absorb_VIT = 536,
		Absorb_AGI = 538,
		Absorb_INT = 540,
		Absorb_MND = 542,
		Absorb_CHR = 544,
		Sleepga_DRK = 546,
		Sleepga_II_DRK = 548,
		Absorb_TP = 550,
		Blind_II_DRK = 552,
		Dread_Spikes = 554,
		Geohelix = 556,
		Hydrohelix = 558,
		Anemohelix = 560,
		Pyrohelix = 562,
		Cryohelix = 564,
		Ionohelix = 566,
		Noctohelix = 568,
		Luminohelix = 570,
		Addle = 572,
		Klimaform = 574,
		Fire_Spirit = 576,
		Ice_Spirit = 578,
		Air_Spirit = 580,
		Earth_Spirit = 582,
		Thunder_Spirit = 584,
		Water_Spirit = 586,
		Light_Spirit = 588,
		Dark_Spirit = 590,
		Carbuncle = 592,
		Fenrir = 594,
		Ifrit = 596,
		Titan = 598,
		Leviathan = 600,
		Garuda = 602,
		Shiva = 604,
		Ramuh = 606,
		Diabolos = 608,
		Odin = 610,
		Alexander = 612,
		Animus_Augeo = 616,
		Animus_Minuo = 618,
		Enlight = 620,
		Endark = 622,
		Enfire_II = 624,
		Enblizzard_II = 626,
		Enaero_II = 628,
		Enstone_II = 630,
		Enthunder_II = 632,
		Enwater_II = 634,
		Monomi_Ichi = 636,
		Aisha_Ichi = 638,
		Katon_Ichi = 640,
		Katon_Ni = 642,
		Katon_San = 644,
		Hyoton_Ichi = 646,
		Hyoton_Ni = 648,
		Hyoton_San = 650,
		Huton_Ichi = 652,
		Huton_Ni = 654,
		Huton_San = 656,
		Doton_Ichi = 658,
		Doton_Ni = 660,
		Doton_San = 662,
		Raiton_Ichi = 664,
		Raiton_Ni = 666,
		Raiton_San = 668,
		Suiton_Ichi = 670,
		Suiton_Ni = 672,
		Suiton_San = 674,
		Utsusemi_Ichi = 676,
		Utsusemi_Ni = 678,
		Utsusemi_San = 680,
		Jubaku_Ichi = 682,
		Jubaku_Ni = 684,
		Jubaku_San = 686,
		Hojo_Ichi = 688,
		Hojo_Ni = 690,
		Hojo_San = 692,
		Kurayami_Ichi = 694,
		Kurayami_Ni = 696,
		Kurayami_San = 698,
		Dokumori_Ichi = 700,
		Dokumori_Ni = 702,
		Dokumori_San = 704,
		Tonko_Ichi = 706,
		Tonko_Ni = 708,
		Tonko_San = 710,
		Paralyga = 712,
		Slowga = 714,
		Hastega = 716,
		Silencega = 718,
		Dispelga = 720,
		Blindga = 722,
		Bindga = 724,
		Sleepga = 726,
		Sleepga_II = 728,
		Breakga = 730,
		Graviga = 732,
		Death = 734,
		Foe_Requiem = 736,
		Foe_Requiem_II = 738,
		Foe_Requiem_III = 740,
		Foe_Requiem_IV = 742,
		Foe_Requiem_V = 744,
		Foe_Requiem_VI = 746,
		Foe_Requiem_VII = 748,
		Foe_Requiem_VIII = 750,
		Horde_Lullaby = 752,
		Horde_Lullaby_II = 754,
		Armys_Paeon = 756,
		Armys_Paeon_II = 758,
		Armys_Paeon_III = 760,
		Armys_Paeon_IV = 762,
		Armys_Paeon_V = 764,
		Armys_Paeon_VI = 766,
		Armys_Paeon_VII = 768,
		Armys_Paeon_VIII = 770,
		Mages_Ballad = 772,
		Mages_Ballad_II = 774,
		Mages_Ballad_III = 776,
		Knights_Minne = 778,
		Knights_Minne_II = 780,
		Knights_Minne_III = 782,
		Knights_Minne_IV = 784,
		Knights_Minne_V = 786,
		Valor_Minuet = 788,
		Valor_Minuet_II = 790,
		Valor_Minuet_III = 792,
		Valor_Minuet_IV = 794,
		Valor_Minuet_V = 796,
		Sword_Madrigal = 798,
		Blade_Madrigal = 800,
		Hunters_Prelude = 802,
		Archers_Prelude = 804,
		Sheepfoe_Mambo = 806,
		Dragonfoe_Mambo = 808,
		Fowl_Aubade = 810,
		Herb_Pastoral = 812,
		Chocobo_Hum = 814,
		Shining_Fantasia = 816,
		Scops_Operetta = 818,
		Puppets_Operetta = 820,
		Travelers_Operetta = 822,
		Gold_Capriccio = 824,
		Devotee_Serenade = 826,
		Warding_Round = 828,
		Goblin_Gavotte = 830,
		Sabotender_Fugue = 832,
		Mogs_Rhapsody = 834,
		Passionate_Aria = 836,
		Advancing_March = 838,
		Victory_March = 840,
		Battlefield_Elegy = 842,
		Carnage_Elegy = 844,
		Massacre_Elegy = 846,
		Sinewy_Etude = 848,
		Dextrous_Etude = 850,
		Vivacious_Etude = 852,
		Quick_Etude = 854,
		Learned_Etude = 856,
		Spirited_Etude = 858,
		Enchanting_Etude = 860,
		Herculean_Etude = 862,
		Uncanny_Etude = 864,
		Vital_Etude = 866,
		Swift_Etude = 868,
		Sage_Etude = 870,
		Logical_Etude = 872,
		Bewitching_Etude = 874,
		Fire_Carol = 876,
		Ice_Carol = 878,
		Wind_Carol = 880,
		Earth_Carol = 882,
		Lightning_Carol = 884,
		Water_Carol = 886,
		Light_Carol = 888,
		Dark_Carol = 890,
		Fire_Carol_II = 892,
		Ice_Carol_II = 894,
		Wind_Carol_II = 896,
		Earth_Carol_II = 898,
		Lightning_Carol_II = 900,
		Water_Carol_II = 902,
		Light_Carol_II = 904,
		Dark_Carol_II = 906,
		Fire_Threnody = 908,
		Ice_Threnody = 910,
		Wind_Threnody = 912,
		Earth_Threnody = 914,
		Lightning_Threnody = 916,
		Water_Threnody = 918,
		Light_Threnody = 920,
		Dark_Threnody = 922,
		Magic_Finale = 924,
		Foe_Lullaby = 926,
		Goddesss_Hymnus = 928,
		Chocobo_Mazurka = 930,
		Maidens_Virelai = 932,
		Raptor_Mazurka = 934,
		Foe_Sirvente = 936,
		Adventurers_Dirge = 938,
		Sentinels_Scherzo = 940,
		Foe_Lullaby_II = 942,
		Refresh_II = 946,
		Cura_II = 948,
		Regen_IV = 954,
		Boost_VIT = 962,
		Boost_AGI = 964,
		Boost_MND = 968,
		Boost_CHR = 970,
		Gain_VIT = 976,
		Gain_AGI = 978,
		Gain_MND = 982,
		Gain_CHR = 984,
		Adloquium = 990,
		Firaja = 992,
		Aeroja = 996,
		Stoneja = 998,
		Waterja = 1002,
		Impact = 1006,
		Myoshu_Ichi = 1014,
		Yurin_Ichi = 1016,
		Migawari_Ichi = 1020,

		Venom_Shell = 1026,
		Maelstrom = 1030,
		Metallic_Body = 1034,
		Screwdriver = 1038,
		MP_Drainkiss = 1042,
		Death_Ray = 1044,
		Sandspin = 1048,
		Smite_of_Rage = 1054,
		Bludgeon = 1058,
		Refueling = 1060,
		Ice_Break = 1062,
		Blitzstrahl = 1064,
		Self_Destruct = 1066,
		Mysterious_Light = 1068,
		Cold_Wave = 1070,
		Poison_Breath = 1072,
		Stinking_Gas = 1074,
		Memento_Mori = 1076,
		Terror_Touch = 1078,
		Spinal_Cleave = 1080,
		Blood_Saber = 1082,
		Digest = 1084,
		Mandibular_Bite = 1086,
		Cursed_Sphere = 1088,
		Sickle_Slash = 1090,
		Cocoon = 1094,
		Filamented_Hold = 1096,
		Pollen = 1098,
		Power_Attack = 1102,
		Death_Scissors = 1108,
		Magnetite_Cloud = 1110,
		Eyes_On_Me = 1114,
		Frenetic_Rip = 1120,
		Frightful_Roar = 1122,
		Hecatomb_Wave = 1126,
		Body_Slam = 1128,
		Radiant_Breath = 1130,
		Helldive = 1134,
		Jet_Stream = 1138,
		Blood_Drain = 1140,
		Sound_Blast = 1144,
		Feather_Tickle = 1146,
		Feather_Barrier = 1148,
		Jettatura = 1150,
		Yawn = 1152,
		Foot_Kick = 1154,
		Wild_Carrot = 1156,
		Voracious_Trunk = 1158,
		Healing_Breeze = 1162,
		Chaotic_Eye = 1164,
		Sheep_Song = 1168,
		Ram_Charge = 1170,
		Claw_Cyclone = 1174,
		Lowing = 1176,
		Dimensional_Death = 1178,
		Heat_Breath = 1182,
		Blank_Gaze = 1184,
		Magic_Fruit = 1186,
		Uppercut = 1188,
		Thousand_Needles = 1190,
		Pinecone_Bomb = 1192,
		Sprout_Smack = 1194,
		Soporific = 1196,
		Queasyshroom = 1198,
		Wild_Oats = 1206,
		Bad_Breath = 1208,
		Geist_Wall = 1210,
		Awful_Eye = 1212,
		Frost_Breath = 1216,
		Infrasonics = 1220,
		Disseverment = 1222,
		Actinic_Burst = 1224,
		Reactor_Cool = 1226,
		Saline_Coat = 1228,
		Plasma_Charge = 1230,
		Temporal_Shift = 1232,
		Vertical_Cleave = 1234,
		Blastbomb = 1236,
		Battle_Dance = 1240,
		Sandspray = 1242,
		Grand_Slam = 1244,
		Head_Butt = 1246,
		Bomb_Toss = 1252,
		Frypan = 1256,
		Flying_Hip_Press = 1258,
		Hydro_Shot = 1262,
		Diamondhide = 1264,
		Enervation = 1266,
		Light_of_Penance = 1268,
		Warm_Up = 1272,
		Firespit = 1274,
		Feather_Storm = 1276,
		Tail_Slap = 1280,
		Hysteric_Barrage = 1282,
		Amplification = 1284,
		Cannonball = 1286,
		Mind_Blast = 1288,
		Exuviation = 1290,
		Magic_Hammer = 1292,
		Zephyr_Mantle = 1294,
		Regurgitation = 1296,
		Seedspray = 1300,
		Corrosive_Ooze = 1302,
		Spiral_Spin = 1304,
		Asuran_Claws = 1306,
		Sub_zero_Smash = 1308,
		Triumphant_Roar = 1310,
		Acrid_Stream = 1312,
		Blazing_Bound = 1314,
		Plenilune_Embrace = 1316,
		Demoralizing_Roar = 1318,
		Cimicine_Discharge = 1320,
		Animating_Wail = 1322,
		Battery_Charge = 1324,
		Leafstorm = 1326,
		Regeneration = 1328,
		Final_Sting = 1330,
		Goblin_Rush = 1332,
		Vanity_Dive = 1334,
		Magic_Barrier = 1336,
		Whirl_of_Rage = 1338,
		Benthic_Typhoon = 1340,
		Auroral_Drape = 1342,
		Osmosis = 1344,
		Quad_Continuum = 1346,
		Fantod = 1348,
		Thermal_Pulse = 1350,
		Empty_Thrash = 1354,
		Dream_Flower = 1356,
		Occultation = 1358,
		Charged_Whisker = 1360,
		Winds_Promyvion = 1362,
		Delta_Thrust = 1364,
		Everyones_Grudge = 1366,
		Reaving_Wind = 1368,
		Barrier_Tusk = 1370,
		Mortal_Ray = 1372,
		Water_Bomb = 1374,
		Heavy_Strike = 1376,
		Dark_Orb = 1378,
		White_Wind = 1380,
		Sudden_Lunge = 1384,
		Quadrastrike = 1386,
		Vapor_Spray = 1388,
		Thunder_Breath= 1390,
		O_Counterstance = 1392,
		Amorphic_Spikes = 1394,
		Wind_Breath = 1396,
		Barbed_Crescent = 1398,
		Thunderbolt = 1472,
		Harden_Shell = 1474,
		Absolute_Terror = 1476,
		Gates_Hades = 1478,
		Tourbillion = 1480,
		Pyric_Bulwark = 1482,
		Bilgestorm = 1484,
		Bloodrake = 1486


	} // @ public enum SpellList : short

	#endregion

	#region AbilityList

	/// <summary>
	/// Ability List
	/// </summary>
	public enum AbilityList : byte {
		Two_Hour = 0,
		Berserk = 1,
		Warcry = 2,
		Defender = 3,
		Aggressor = 4,
		Provoke = 5,
		Warriors_Charge = 6,
		Tomahawk = 7,
		Retaliation = 8,
		Restraint = 9,
		Blood_Rage = 11,
		Focus = 13,
		Dodge = 14,
		Chakra = 15,
		Boost = 16,
		Counterstance = 17,
		Chi_Blast = 18,
		Mantra = 19,
		Formless = 20,
		Perfect_Counter = 22,
		Divine_Seal = 26,
		Martyr = 27,
		Devotion = 28,
		Afflatus_Solace = 29,
		Afflatus_Misery = 30,
		Impetus = 31,
		Divine_Caress = 32,
		Sacrosanctity = 33,
		Enmity_Douse = 34,
		Manawell = 35,
		Saboteur = 36,
		Spontaneity = 37,
		Elemental_Seal = 38,
		Mana_Wall = 39,
		Conspirator = 40,
		Sepulcher = 41,
		Palisade= 42,
		Arcane_Crest = 43,
		Scarlet_Delirium = 44,
		Spur = 45,
		Run_Wild = 46,
		Tenuto = 47,
		Marcato = 48,
		Convert = 49,
		Composure = 50,
		Bounty_Shot = 51,
		Decoy_Shot = 52,
		Hamanoha = 53,
		Hagakure = 54,
		Issekigan = 57,
		Dragon_Breaker = 58,
		Steal = 61,
		Flee = 62,
		Hide = 63,
		Sneak_Attack = 64,
		Mug = 65,
		Trick_Attack = 66,
		Assassins_Charge = 67,
		Feint = 68,
		Accomplice = 69,
		Steady_Wing = 70,
		Mana_Cede = 71,
		Shield_Bash = 73,
		Holy_Circle = 74,
		Sentinel = 75,
		Cover = 76,
		Rampart = 77,
		Fealty = 78,
		Chivalry = 79,
		Divine_Emblem = 80,
		Unbridled_Learning = 81,
		Triple_Shot = 84,
		Souleater = 85,
		Arcane_Circle = 86,
		Last_Resort = 87,
		Weapon_Bash = 88,
		Dark_Seal = 89,
		Diabolic_Eye = 90,
		Nether_Void = 91,
		Charm = 97,
		Gauge = 98,
		Tame = 99,
		Fight = 100,
		Stay_Heel_Leave = 101,
		Sic = 102,
		Reward = 103,
		Call_Beast = 104,
		Feral_Howl = 105,
		Killer_Instinct = 106,
		Snarl = 107,
		Nightingale = 109,
		Troubadour = 110,
		Pianissimo = 112,
		Cooldown = 114,
		Deus_Ex_Automata = 115,
		Scavenge = 121,
		Shadowbind = 122,
		Camouflage = 123,
		Sharpshot = 124,
		Barrage = 125,
		Unlimited_Double_Shot = 126,
		Stealth_Shot = 127,
		Flashy_Shot = 128,
		Velocity_Shot = 129,
		Konzen_ittai = 132,
		Third_Eye = 133,
		Meditate = 134,
		Warding_Circle = 135,
		Shikikoyo = 136,
		Blade_Bash = 137,
		Hasso = 138,
		Seigan = 139,
		Sekkanoki = 140,
		Sengikori = 141,
		Sange = 145,
		Yonin = 146,
        Innin = 147,
        Futae = 148,
		Ancient_Circle = 157,
		Jump = 158,
		High_Jump = 159,
		Super_Jump = 160,
		Dismiss = 161,
		Spirit_Link = 162,
		Call_Wyvern = 163,
		Deep_Breathing = 164,
		Angon = 165,
		Assault = 170,
		Retreat = 171,
		Release = 172,
		Blood_Pact_Rage = 173,
		Blood_Pact_Ward = 174,
		Elemental_Siphon = 175,
		Avatars_Favor = 176,
		Chain_Affinity = 181,
		Burst_Affinity = 182,
		Convergence = 183,
		Diffusion = 184,
		Efflux = 185,
		Roll = 193,
		Double_Up = 194,
		Quick_Draw = 195,
		Random_Deal = 196,
		Snake_Eye = 197,
		Fold_Passen = 198,
		// QuickDraw = 199?
		Activate = 205,
		Repair = 206,
		Deploy = 207,
		Deactivate = 208,
		Retrieve = 209,
		Maneuver = 210,
		Role_Reversal = 211,
		Ventriloquy = 212,
		Tactical_Switch = 213,
		Maintenance = 214,
		Sambas = 216,
		Waltzes = 217,
		Jigs = 218,
		Saber_Dance = 219,
		Steps = 220,
		Flourishes_I = 221,
		Flourishes_II = 222,
		No_Foot_Rise = 223,
		Fan_Dance = 224,
		Flourishes_III = 226,
		Light_Arts = 228,
		Modus = 230,
		Stratagems = 231,
		Dark_Arts = 232,
		Sublimation = 234,
		Enlightenment = 235,
		Presto = 236,
		Libra = 237,
		Smiting_Breath = 238,
		Restoring_Breath = 239,
		Bully = 240,
		Ready = 251,
		Unknown = 255

	} // @ public enum AbilityList : byte

	#endregion

	#region Weather

	/// <summary>
	/// Weather Types
	/// </summary>
	public enum Weather : byte {
		Clear = 0,
		Sunny = 1,
		Cloudy = 2,
		Fog = 3,
		Fire = 4,
		Fire_Double = 5,
		Water = 6,
		Water_Double = 7,
		Earth = 8,
		Earth_Double = 9,
		Wind = 10,
		Wind_Double = 11,
		Ice = 12,
		Ice_Double = 13,
		Lightning = 14,
		Lightning_Double = 15,
		Light = 16,
		Light_Double = 17,
		Dark = 18,
		Dark_Double = 19

	} // @ public enum Weather : byte

	#endregion

	#region Inventory related
	[System.Flags]
	public enum InventoryType : uint {
		/// <summary>
		/// By itself means NONE.
		/// </summary>
		None = 0,
		/// <summary>
		/// Inventory
		/// </summary>
		Inventory = 1,
		/// <summary>
		/// Mog Safe
		/// </summary>
		Safe = 2,
		/// <summary>
		/// Storage
		/// </summary>
		Storage = 4,
		/// <summary>
		/// Mog Locker
		/// </summary>
		Locker = 8,
		/// <summary>
		/// Mog Satchel
		/// </summary>
		Satchel = 16,
		/// <summary>
		/// Mog Sack
		/// </summary>
		Sack = 32,
		/// <summary>
		/// Temporary Items
		/// </summary>
		Temp = 64,
		/// <summary>
		/// Future expansion (no effect)
		/// </summary>
		Unknown = 128,
		/// <summary>
		/// Future expansion (no effect)
		/// </summary>
		Unknown2 = 256,
		/// <summary>
		/// Inventory, Satchel, And Sack
		/// </summary>
		AllBagsNoTemp = Inventory | Satchel | Sack,
		/// <summary>
		/// Inventory, Satchel, Sack, Temporary Items
		/// </summary>
		AllBagsPlusTemp = Inventory | Satchel | Sack | Temp,
		/// <summary>
		/// Everything including Temporary Items
		/// </summary>
		AllPlusTemp = Inventory | Safe | Storage | Locker | Satchel | Sack | Temp,
		/// <summary>
		/// AllPlusTemp (Everything including Temporary Items)
		/// </summary>
		All = Inventory | Safe | Storage | Locker | Satchel | Sack | Temp,
		/// <summary>
		/// Everything not including temporary items
		/// </summary>
		AllNoTemp = Inventory | Safe | Storage | Locker | Satchel | Sack
	}
	#endregion

	#region Treasure

	/// <summary>
	/// Status of the item in the treasure pool
	/// </summary>
	public enum TreasureStatus : byte {
		NoAction = 0,
		Pass = 1,
		Lot = 2

	} // @ public enum TreasureStatus

	#endregion

	#region Player related

	/// <summary>
	/// Enum for rank levels
	/// </summary>
	public enum CraftRank : int {
		Amateur,
		Recruit,
		Initiate,
		Novice,
		Apprentice,
		Journeyman,
		Craftsman,
		Artisan,
		Adept,
		Veteran
	}

	/// <summary>
	/// Craft types
	/// </summary>
	public enum Craft : byte {
		Alchemy,
		Bonecrafting,
		Clothcraft,
		Cooking,
		Fishing,
		Goldsmithing,
		Leathercraft,
		Smithing,
		Woodworking,
		Synergy

	} // @ public enum Craft : byte

	/// <summary>
	/// Flags for bitsetting to determinte skill type.
	/// </summary>
	[System.Flags]
	public enum MagicOrCombat : uint {
		Magic = 0x1000,
		Combat = 0x2000
	}

	/// <summary>
	/// Magic skill types
	/// </summary>
	public enum MagicSkill : byte {
		BlueMagic,
		Dark,
		Divine,
		Enfeebling,
		Enhancing,
		Elemental,
		Healing,
		Ninjitsu,
		Singing,
		String,
		Summoning,
		Wind

	} // @ public enum MagicSkill : byte

	/// <summary>
	/// Combat skill types
	/// </summary>
	public enum CombatSkill : byte {
		HandToHand,
		Dagger,
		Sword,
		GreatSword,
		Axe,
		GreatAxe,
		Scythe,
		Polearm,
		Katana,
		GreatKatana,
		Club,
		Staff,
		Archery,
		Marksmanship,
		Throwing,
		Guarding,
		Evasion,
		Shield,
		Parrying

	} // @ public enum CombatSkill : byte

	/// <summary>
	/// Equipment Slot
	/// </summary>
	public enum EquipSlot : short {
		Main = 0,
		Shield = 1,
		Range = 2,
		Ammo = 3,
		Head = 4,
		Body = 5,
		Hands = 6,
		Legs = 7,
		Feet = 8,
		Neck = 9,
		Waist = 10,
		EarLeft = 11,
		EarRight = 12,
		RingLeft = 13,
		RingRight = 14,
		Back = 15

	} // @ public enum EquipSlot : short

	/// <summary>
	/// Alignment in which the fishing rod can be in
	/// </summary>
	public enum RodAlign : byte {
		Err = 0,
		Center = 1,
		Left = 2,
		Right = 3

	} // @ public enum RodAlign : byte

	/// <summary>
	/// View mode the player is in
	/// </summary>
	public enum ViewMode : byte {
		ThirdPerson,
		FirstPerson

	} // @ public enum ViewMode : byte

	/// <summary>
	/// Nation person is in
	/// </summary>
	public enum Nation : byte {
		SandOria,
		Bastok,
		Windurst

	} // @ public enum Nation : byte

	public enum Race : byte {
		HumeMale,
		HumeFemale,
		ElvaanMale,
		ElvaanFemale,
		TarutaruMale,
		TarutaruFemale,
		MithraFemale,
		GalkaMale,
		Unknown,
	}
	#endregion

	#region Timer Related

	/// <summary>
	/// The different moon phases
	/// </summary>
	public enum MoonPhase : byte {
		New,
		WaxingCrescent,
		WaxingCrescent2,
		FirstQuarter,
		WaxingGibbous,
		WaxingGibbous2,
		Full,
		WaningGibbous,
		WaningGibbous2,
		LastQuarter,
		WaningCrescent,
		WaningCrescent2,
		Unknown

	} // @ public enum MoonPhase

	/// <summary>
	/// Names of the days of the week
	/// </summary>
	public enum Weekday {
		Firesday,
		Earthsday,
		Watersday,
		Windsday,
		Iceday,
		Lightningday,
		Lightsday,
		Darksday,
		Unknown

	} // @ public enum Weekday

	#endregion

	#region KeyCodes

	/// <summary>
	/// Key Code sent to windower
	/// </summary>
	public enum KeyCode {
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		//~~~~~~~~~These Here Are The Most Important FFXI Keys~~~~~~~~~
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		EscapeKey = 1,

		EnterKey = 28,
		TabKey = 15,

		UpArrow = 200,
		LeftArrow = 203,
		RightArrow = 205,
		DownArrow = 208,
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		//~~~~~~~These Here Are The NumPad Keys On Your Keyboard~~~~~~~
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		MainNumlockKey = 69,

		NP_Number0 = 82,
		NP_Number1 = 79,
		NP_Number2 = 80,
		NP_Number3 = 81,
		NP_Number4 = 75,
		NP_Number5 = 76,
		NP_Number6 = 77,
		NP_Number7 = 71,
		NP_Number8 = 72,
		NP_Number9 = 73,

		NP_Forwardslash = 181,
		NP_MultiplyKey = 55,
		NP_MinusKey = 74,
		NP_AdditionKey = 78,
		NP_EnterKey = 156,
		NP_PeriodKey = 83,
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		//~~~These Here Are The Letters From A to Z On Your Keyboard~~~
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		LetterA = 30,
		LetterB = 48,
		LetterC = 46,
		LetterD = 32,
		LetterE = 18,
		LetterF = 33,
		LetterG = 34,
		LetterH = 35,
		LetterI = 23,
		LetterJ = 36,
		LetterK = 37,
		LetterL = 38,
		LetterM = 50,
		LetterN = 49,
		LetterO = 24,
		LetterP = 25,
		LetterQ = 16,
		LetterR = 19,
		LetterS = 31,
		LetterT = 20,
		LetterU = 22,
		LetterV = 47,
		LetterW = 17,
		LetterX = 45,
		LetterY = 21,
		LetterZ = 44,
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		//~~~These Here Are The Numbers From 0 to 9 On Your Keyboard~~~
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		Number1 = 2,
		Number2 = 3,
		Number3 = 4,
		Number4 = 5,
		Number5 = 6,
		Number6 = 7,
		Number7 = 8,
		Number8 = 9,
		Number9 = 10,
		Number0 = 11,
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		//~~These Here Are The F Keys From F1 to F12 On Your Keyboard~~
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		F1Key = 59,
		F2Key = 60,
		F3Key = 61,
		F4Key = 62,
		F5Key = 63,
		F6Key = 64,
		F7Key = 65,
		F8Key = 66,
		F9Key = 67,
		F10Key = 68,
		F11Key = 87,
		F12Key = 88,

		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		//~~~~These Here Are Ones That You Should Not Need But Here~~~~
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		MinusKey = 12,
		EqualsKey = 13,
		BackspaceKey = 14,
		LeftBracket = 26,
		RightBracket = 27,
		LeftCtrlKey = 29,
		Semicolon = 39,
		Apostrophe = 40,
		Accentgrave = 41,
		LeftShift = 42,
		Backslash = 43,
		CommaKey = 51,
		PeriodKey = 52,
		ForwardslashKEy = 53,
		RightShift = 54,
		ScrollLock = 70,
		LeftAltKey = 56,
		Spacebar = 57,
		CapsLock = 58,
		RightControlKey = 157,
		RightAltKey = 184,
		InsertKey = 210,
		DeleteKey = 211,
		LeftWindowKey = 219,
		RightWindowKey = 220

		//Calculator = &HA1
		//MuteKey = &HA0
		//PlayNPauseKey = &HA2
		//StopMedia = &HA
		//VolumeDown = &HAE
		//VolumeUp = &HB0
		//NextMediaTrack = &HED
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	}

	#endregion

	#region NPC

	/// <summary>
	/// Type of NPC returned from GetNPCType
	/// </summary>
	public enum NPCType : byte {
		Player,
		NPC,
		Mob,
		InanimateObject

	} // @ public enum NPCType

	#endregion
}

