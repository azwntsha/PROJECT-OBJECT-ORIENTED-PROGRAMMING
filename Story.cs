using System;
namespace PrinceAdventureGame
{
  class Story
{
    //--------------- NARRATOR METHOD ----------------
    public static void Narrator(string text)
    {
        foreach (char letter in text)
        {
            if (Console.KeyAvailable)
            {
                Console.ReadKey(true); //Press any ket to skip animation
                Console.Write(text.Substring(text.IndexOf(letter)));
                break;
            }

            Console.Write(letter);
            Thread.Sleep(35); //speed of animation
        }
        Console.WriteLine();
    }

    public static void ShowIntro()
    {
        Console.Clear();
        Console.WriteLine("======================================================================");
        Console.WriteLine("                       THE PRINCE OF ELDORIA\t\t\t");
        Console.WriteLine("======================================================================");
        Console.WriteLine(@" 
                                         ____                                         
                                      .-""    `-.      ,                               
                                    .'          '.   /j\                              
                                   /              \,/:/#\                /\           
                                  ;              ,//' '/#\              //#\          
                                  |             /' :   '/#\            /  /#\         
                                  :         ,  /' /'    '/#\__..--""""""""/    /#\__      
                                   \       /'\'-._:__    '/#\        ;      /#, """"""---
                                    `-.   / ;#\']"" ; """"""--./#J       ':____...!       
                                       `-/   /#\  J  [;[;[;Y]         |      ;        
        """"""""""""---....             __.--""/    '/#\ ;   "" ""  |     !    |   #! |        
                     """"--.. _.--""""     /      ,/#\'-..____.;_,   |    |   '  |        
                           ""-.        :_....___,/#} ""####"" | '_.-"",   | #['  |        
                              '-._      |[;[;[;[;|         |.;'  /;\  |      |        
                              ,   `-.   |        :     _   .;'    /;\ |   #"" |        
                              !      `._:      _  ;   ##' .;'      /;\|  _,  |        
                             .#\""""""---..._    ';, |      .;{___     /;\  ]#' |__....--
                  .--.      ;'/#\         \    ]! |       ""| , """"""--./_J    /         
                 /  '%;    /  '/#\         \   !' :        |!# #! #! #|    :`.__      
                i__..'%] _:_   ;##J         \      :""#...._!   '  ""  ""|__  |    `--.._
                 | .--"""""" !|""""""""  |""""""----...J     | '##"""" `-._       |  """"""---.._    
             ____: |      #|      |         #|     |          ""]      ;   ___...-""T,  
            /   :  :      !|      |   _______!_    |           |__..--;""""""     ,;MM;  
           :____| :    .-.#|      |  /\      /#\   |          /'               ''MM;  
            |"""""": |   /   \|   .----+  ;      /#\  :___..--"""";                  ,'MM; 
           _Y--:  |  ;     ;.-'      ;  \______/#: /         ;                  ''MM; 
          /    |  | ;_______;     ____!  |""##""""""MM!         ;                    ,'MM;
         !_____|  |  |""#""#""|____.'""""##""  |       :         ;                     ''MM  
          | """"""""--!._|     |##""""         !       !         :____.....-------"""""""""""" |'
          |          :     |______                        ___!_ ""#""""#""""#""""""#""""""#""""""|  
        __|          ;     |MM""MM""""""""""---..._______...--""""MM""MM]                   |   
          ""\-.      :      |#                                  :                   |  
            /#'.    |      /##,                                !                   |  
           .',/'\   |       #:#,                                ;       .==.       |  
          /""\'#""\',.|       ##;#,                               !     ,'||||',     |  
                /;/`:       ######,          ____             _ :     M||||||M     |  
               ###          /;""\.__""-._   """"""                   |===..M!!!!!!M_____|  
                                   `--..`--.._____             _!_                    
                                                  `--...____,=""_.'`-.____ 
        ");



        Narrator("The Kingdom of Eldoria had long been a land of peace.");
        Narrator("Under the rule of King Aldren and Queen Lyra, the kingdom prospered and its people lived without fear");
        Narrator("One night, during a grand royal celebration, the sky suddenly darkened.");
        Narrator("A violent storm formed above the castle, and a mysterious portal opened in the throne hall.");
        Narrator("From the portal emerged the dark sorcerer Maldrath and his army of shadow creatures");
        Narrator("In the chaos, Maldrath defeated the royal guards and kidnapped Queen Lyra.");
        Narrator("Before disappearing into the darkness, he left a message:");
        Narrator(" ");
        Narrator("    “If you wish to see the queen again, send your bravest warrior to the Shadow Fortress… if he survives the journey.”");
        Narrator(" ");
        Narrator("The king was gravely injured during the attack, leaving the kingdom in despair.");
        Narrator("With the royal army scattered, the young Prince Aric swore an oath to rescue the queen and restore peace to the kingdom");
        Narrator("The prince sets out on a dangerous journey.\r\n");

        Console.WriteLine("\n(Press any key to skip narration)");
        Console.WriteLine("\nPress any key to begin your destiny...");
        Console.ReadKey();

        Narrator(" ");
        Narrator("The prince arrives at the Ruins of Valtheris, ");
        Narrator("an ancient city abandoned long ago.\r\n");
        Console.WriteLine(@" 
                  .----------.
                  :`.---------`.          .-----------.
                  '.|_.------..'          :`.----------`.  .-----.._
                   | ||    | ||           `.|__.------..' |`--..__.'|
                   | ||__..| ||-._ ___    | | |.... | ||-.|`--.._ |.'----.
            _ .:::/ / |    | ||   ___  `--| | |  _  | ||  | | | | |\      `.
          .` `.  -'.|_|    `.||  /`.  `.  '.|_\     `.|/  | | | | ||`.      `.
         / `. _`.  `-   _     _  |  `.__`   _           _ `.|.' | || |\       \
         \   /   \ _            _ `. |   |     `--   _          '.|| | `.______.
         /`. |   |  .'`---...____   `'---' _       _          _    | | |'------'
         | /`.-_.' |`---...___ . |  `--      ______________________`.|_| |  ` |
         | | | '|  `---...____|.'    _     |`._-_______-______-_____`. | |    |
        .| | |  | _ |  .---. | '| `-       '.|  ' ' '  '  '  '  ' ' ' || |    |  
        ' `| |  |   /  ||  |'|  |      _    |'.__.-------.__.-----.__.'| | .- | \
        |  | |' |   |  ||  | |  |   `--     | |..|    |  |''|   | |::| `.|____| |
        \   `|__| _ |  || _| '. | .`-._     | |  |    |  |  |   | |  | `--     .'
         '.         |  ||  |  | | |`.  `. _ | |  |    |  |  |   | |  |   _    `.
           '-. _    \__|   |  | | |  `.__`. | |  | __ |  |  |___| |  |  `-- _  |
              `''''.    _  `-.|.' '.  |   |  `|__| ___ `.|__| _ `.|__|        /
                   :    _    _      `.|_.-'   `-             _          _   .'
                    `-._______.::.__     `-     _      _       ___.:::.__.-'
                                    `----.....__..------------`
        ");
        Narrator("While exploring the ruins,");
        Narrator("Aric discovers an ancient scroll that reveals a shocking truth.");
        Console.ReadKey();
        Console.WriteLine(@" 
        _______________________
      =(__    ___      __     _)=
        |                     |
        |                     |
        |       Eldorian      |
        |     Royal Magical   |
        |        Academy      |
        |                     |
        |                     |
        |                     |
        |                     |
        |__    ___   __    ___|
      =(_______________________)=");
        Narrator("Maldrath was once the royal mage of Eldoria, ");
        Narrator("but he was exiled after experimenting with forbidden magic.\r\n");
        Console.ReadKey();
        Narrator("At the center of the ruins, the prince faces the Undead General, ");
        Narrator("a powerful warrior cursed to guard the ruins forever.\r\n");
        Console.WriteLine(@" 
           , ,, ,                              
           | || |    ,/  _____  \.             
           \_||_/    ||_/ _  _\_||             
             ||       \_| . . |_/              
             ||         |  L  |                
            ,||         |.===.|                
            |>|      ___`>  -<'___             
            |>|\    /             \            
            \>| \  /  ,    .    .  |           
             ||  \/  /|    |    |  |           
             ||\  ` / | ___|___ |  |     (     
          (( || `--'  | _______ |  |     ))  ( 
        (  )\|| (  )\ | - --- - | -| (  ( \  ))
        (\/  || ))/ ( | -- - -- |  | )) )  \(( 
         ( ()||((( ())|         |  |( (( () )
        ");
        Narrator(" ");
        Console.ReadKey();

    }

    public static void AfterLevel1()
    {
        Narrator("After defeating the general, ");
        Narrator("the prince obtains an ancient sword infused with holy magic,  ");
        Console.WriteLine(@" 
                       <>
                     //
                   //
        <>(((((((({@}::::::::::::::::::::::::::::::::::::::::::::::::::::======->
                   \\
                     \\
                       <>
        ");
        Narrator("capable of harming powerful dark creatures. ");
        Console.ReadKey();
        Narrator("The prince begins to question the past of the kingdom. ");
        Narrator(" ");
        Narrator("He continuos the journey........ ");
        Console.ReadKey();

        Narrator("The next obstacle is the deadly Mount Ignivar, ");
        Narrator(" where rivers of lava flow and fire monsters roam. ");
        Console.WriteLine(@" 
            .                  .-.    .  _   *     _   .
                   *          /   \     ((       _/ \       *    .
                 _    .   .--'\/\_ \     `      /    \  *    ___
             *  / \_    _/ ^      \/\'__        /\/\  /\  __/   \ *
               /    \  /    .'   _/  /  \  *' /    \/  \/ .`'\_/\   .
          .   /\/\  /\/ :' __  ^/  ^/    `--./.'  ^  `-.\ _    _:\ _
             /    \/  \  _/  \-' __/.' ^ _   \_   .'\   _/ \ .  __/ \
           /\  .-   `. \/     \ / -.   _/ \ -. `_/   \ /    `._/  ^  \
          /  `-.__ ^   / .-'.--'    . /    `--./ .-'  `-.  `-. `.  -  `.
        @/        `.  / /      `-.   /  .-'   / .   .'   \    \  \  .-  \%
        @&8jgs@@%% @)&@&(88&@.-_=_-=_-=_-=_-=_.8@% &@&&8(8%@%8)(8@%8 8%@)%
        @88:::&(&8&&8:::::%&`.~-_~~-~~_~-~_~-~~=.'@(&%::::%@8&8)::&#@8::::
        `::::::8%@@%:::::@%&8:`.=~~-.~~-.~~=..~'8::::::::&@8:::::&8:::::'
         `::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::.'");
        Console.ReadKey();

        Narrator("During the climb, ");
        Narrator("the prince meets a mysterious traveler");
        Console.WriteLine(@" 
                (                 ,&&&.
                )                .,.&&
               (  (              \=__/
                   )             ,'-'.
             (    (  ,,      _.__|/ /|
              ) /\ -((------((_|___/ |
            (  // | (`'      ((  `'--|
          _ -.;_/ \\--._      \\ \-._/.
         (_;-// | \ \-'.\    <_,\_\`--'|
         ( `.__ _  ___,')      <_,-'__,'
          `'(_ )_)(_)_)'"); 
        Console.ReadKey();
        Narrator("He warns the Prince:\r\n ");
        Narrator("“The darkness you seek was once created by the kingdom you protect.”\r\n");
        Console.ReadKey();

        Narrator("At the summit of the mountain, ");
        Narrator("the prince faces the ancient Fire Dragon, awakened by Maldrath’s magic.\r\n ");
        Console.WriteLine(@"              '
                      __
                  _.-'.-'-.__
               .-'.       '-.'-._ __.--._
        -..'\,-,/..-  _         .'   \   '----._
         ). /_ _\' ( ' '.         '-  '/'-----._'-.__
         '.<a} {a>.'     '-r   _      .-.       '-._ \
         '.\. Y .).'       ( .'  .      .\          '\'.
         .-')'|'/'-.        \)    )      '',_      _.c_.\
           .<, ,>.          |   _/\        . ',   :   : \\
          .' \_/ '.        /  .'   |          '.     .'  \)
                          / .-'    '-.        : \   _;   ||
                         / /    _     \_      '.'\ ' /   ||
                        /.'   .'        \_      .|   \   \|
                       / /   /      __.---'      '._  ;  ||
                      /.'  _:-.____< ,_           '.\ \  ||
                     // .-'     '-.__  '-'-\_      '.\/_ \|
                    ( };====.===-==='        '.    .  \\: \
                     \\ '._        /          :   ,'   )\_ \
                      \\   '------/            \ .    /   )/
                       \|        _|             )Y    |   /
                        \\      \             .','   /  ,/
                         \\    _/            /     _/
                          \\   \           .'    .'
                           '| '1          /    .'
                             '. \        |:    /
                               \ |       /', .'
                                \(      ( ;z'
                                 \:      \ '(_
                                  \_,     '._ '-.___
                                              '-' -.\
        ");
        Console.ReadKey();
    }

    public static void AfterLevel2()
    {
        Narrator("The dragon attacks fiercely, ");
        Narrator(" but the prince manages to defeat it using the holy sword. ");
        Console.ReadKey();
        Narrator("Before dying, ");
        Narrator("the dragon reveals another secret:\r\n ");
        Narrator("............ ");
        Narrator("“The queen carries a power the sorcerer desires.” ");
        Console.ReadKey();
        Narrator("Now the prince realizes the kidnapping may be part of a much bigger plan.\r\n ");
        Narrator("He keeps moving forward. ");
        Console.ReadKey();
        Narrator("Beyond the mountain lies the land surrounding Shadow Fortress. ");
        Console.WriteLine(@"
           .         .          /\      .:  *       .          .              .
                         *    .'  `.      .     .     *      .                  .
          :             .    /      \  _ .________________  .                    .
               |            `.+-~~-+.'/.' `.^^^^^^^^\~~~~~\.                      .
         .    -*-   . .       |u--.|  /     \~~~~~~~|~~~~~|
               |              |   u|.'       `."" ""  |"" "" ""|                        .
            :            .    |.u-./ _..---.._ \"" "" | "" "" |
           -*-            *   |    ~-|U U U U|-~____L_____L_                      .
            :         .   .   |.-u.| |..---..|""//// ////// /\       .            .
                  .  *        |u   | |       |// /// // ///==\     / \          .
         .          :         |.--u| |..---..|//////~\////====\   /   \       .
              .               | u  | |       |~~~~/\u |~~|++++| .`+~~~+'  .
                              |.-|~U~U~|---..|u u|u | |u ||||||   |  U|
                           /~~~~/-\---.'     |===|  |u|==|++++|   |   |
                  aaa      |===| _ | ||.---..|u u|u | |u ||HH||U~U~U~U~|        aa@@
             aaa@@@@@@aa   |===|||||_||      |===|_.|u|_.|+HH+|_/_/_/_/aa    a@@@@@@
         aa@@@@@@@@@@@@@@a |~~|~~~~\---/~-.._|--.---------.~~~`.__ _.@@@@@@a    ~~~~
           ~~~~~~    ~~~    \_\\ \  \/~ //\  ~,~|  __   | |`.   :||  ~~~~
                             a\`| `   _//  | / _| || |  | `.'  ,''|     aa@@@@@@@a
         aaa   aaaa       a@@@@\| \  //'   |  // \`| |  `.'  .' | |  aa@@@@@@@@@@@@@
        @@@@@a@@@@@@a      ~~~~~ \\`//| | \ \//   \`  .-'  .' | '/      ~~~~~~~  ~~
        @S.C.E.S.W.@@@@a          \// |.`  ` ' /~  :-'   .'|  '/~aa
        ~~~~~~ ~~~~~~         a@@@|   \\ |   // .'    .'| |  |@@@@@@a
                            a@@@@@@@\   | `| ''.'     .' | ' /@@@@@@@@@a");
        Console.ReadKey();
        Narrator("This area is filled with the sorcerer’s elite forces. ");
        Console.ReadKey();
        Narrator("The prince fights through wave after wave of enemies. ");
        Console.WriteLine(@" 
                                /|
                              /'||
                             |  ||
                             |  ||
                             |  ||
                             |  ||
                             |  ||
                             |  ||
                             |  ||
                             |  ||
                             |  ||
                             |  ||
                             |  ||
                             |  ||
                             |  ||
                             |  ||
                             |  ||         __.--._
                             |  ||      /~~   __.-~\ _
                             |  ||  _.-~ / _---._ ~-\/~\
                             |  || // /  /~/  .-  \  /~-\
                             |  ||((( /(/_(.-(-~~~~~-)_/ |
                             |  || ) (( |_.----~~~~~-._\ /
                             |  ||    ) |              \_|
                             |  ||     (| =-_   _.-=-  |~)        ,
                             |  ||      | `~~ |   ~~'  |/~-._-'/'/_,
                             |  ||       \    |        /~-.__---~ , ,
                             |  ||       |   ~-''     || `\_~~~----~
                             |  ||_.ssSS$$\ -====-   / )\_  ~~--~
                     ___.----|~~~|%$$$$$$/ \_    _.-~ /' )$s._
            __---~-~~        |   |%%$$$$/ /  ~~~~   /'  /$$$$$$$s__
          /~       ~\    ============$$/ /        /'  /$$$$$$$$$$$SS-.
        /'      ./\\\\\\_( ~---._(_))$/ /       /'  /$$$$%$$$$$~      \
        (      //////////(~-(..___)/$/ /      /'  /$$%$$%$$$$'         \
         \    |||||||||||(~-(..___)$/ /  /  /'  /$$$%$$$%$$$            |
          `-__ \\\\\\\\\\\(-.(_____) /  / /'  /$$$$%$$$$$%$             |
              ~~""""""""""""""""""""-\.(____) /   /'  /$$$$$%%$$$$$$\_            /
                            $|===|||  /'  /$$$$$$$%%%$$$$$( ~         ,'|
                        __  $|===|%\/'  /$$$$$$$$$$$%%%%$$|        ,''  |
                       ///\ $|===|/'  /$$$$$$%$$$$$$$%%%%$(            /'
                        \///\|===|  /$$$$$$$$$%%$$$$$$%%%%$\_-._       |
                         `\//|===| /$$$$$$$$$$$%%%$$$$$$-~~~    ~      /
                           `\|-~~(~~-`$$$$$$$$$%%%///////._       ._  |
                           (__--~(     ~\\\\\\\\\\\\\\\\\\\\        \ \
                           (__--~~(       \\\\\\\\\\\\\\\\\\|        \/
                            (__--~(       ||||||||||||||||||/       _/
                             (__.--._____//////////////////__..---~~
                             |   """"""""'''''           ___,,,,ss$$$%
                            ,%\__      __,,,\sssSS$$$$$$$$$$$$$$%%
                          ,%%%%$$$$$$$$$$\;;;;\$$$$$$$$$$$$$$$$%%%$.
                         ,%%%%%%$$$$$$$$$$%\;;;;\$$$$$$$$$$$$%%%$$$$
                       ,%%%%%%%%$$$$$$$$$%$$$\;;;;\$$$$$$$$$%%$$$$$$,
                      ,%%%%%%%%%$$$$$$$$%$$$$$$\;;;;\$$$$$$%%$$$$$$$$
                     ,%%%%%%%%%%%$$$$$$%$$$$$$$$$\;;;;\$$$%$$$$$$$$$$$
                     %%%%%%%%%%%%$$$$$$$$$$$$$$$$$$\;;;$$$$$$$$$$$$$$$
                       """"==%%%%%%%$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$SV""
                                   $$$$$$$$$$$$$$$$$$$$====""""""""
                                     """"""""""""""""""~~~~
        ");
        Console.ReadKey();
        Narrator("The Shadow Commander,Alucard, Maldrath’s most loyal warrior appears..... ");
        Console.WriteLine(@" 
                                   __.--|~|--.__           Alucard             ,,;/;
                                 /~     | |    ;~\     The Shadow Commander ,;;;/;;'
                                /|      | |    ;~\\                      ,;;;;/;;;'
                               |/|      \_/   ;;;|\                    ,;;;;/;;;;'
                               |/ \          ;;;/  )                 ,;;;;/;;;;;'
                           ___ | ______     ;_____ |___....__      ,;;;;/;;;;;'
                     ___.-~ \\(| \  \.\ \__/ /./ /:|)~   ~   \   ,;;;;/;;;;;'
                 /~~~    ~\    |  ~-.     |   .-~: |//  _.-~~--,;;;;/;;;;;'
                (.-~___     \.'|    | /-.__.-\|::::| //~     ,;;;;/;;;;;'
                /      ~~--._ \|   /          `\:: |/      ,;;;;/;;;;;'
          .-|            ~~|   |  /V""""""""V\ |:  |     ,;;;;/;;;;;' \
            /                   \  |  ~`^~~^'~ |  /    ,;;;;/;;;;;'    ;
           (        \             \|`\._____./'|/    ,;;;;/;;;;;'      '\
          / \        \                             ,;;;;/;;;;;'     /    |
         |            |                          ,;;;;/;;;;;'      |     |
        |`-._          |                       ,;;;;/;;;;;'              \
        |             /                      ,;;;;/;;;;;'  \              \__________
        (             )                 |  ,;;;;/;;;;;'      |        _.--~
         \          \/ \              ,  ;;;;;/;;;;;'       /(     .-~_..--~~~~~~~~~~
         \__         '  `       ,     ,;;;;;/;;;;;'    .   /  \   / /~
         /          \'  |`._______ ,;;;;;;/;;;;;;'    /   :    \/'/'       /|_/|   ``|
        | _.-~~~~-._ |   \ __   .,;;;;;;/;;;;;;' ~~~~'   .'    | |       /~ (/\/    ||
        /~ _.-~~~-._\    /~/   ;;;;;;;/;;;;;;;'          |    | |       / ~/_-'|-   /|
        (/~         \| /' |   ;;;;;;/;;;;;;;;            ;   | |       (.-~;  /-   / |
        |            /___ `-,;;;;;/;;;;;;;;'            |   | |      ,/)  /  /-   /  |
         \            \  `-.`---/;;;;;;;;;' |          _'   | |    /'('  /  /|- _/  //
           \           /~~/ `-. |;;;;;''    ______.--~~ ~\  | |  ,~)')  /   | \~-==//
             \      /~(   `-\  `-.`-;   /|    ))   __-####\ | |   (,   /|    |  \
               \  /~.  `-.   `-.( `-.`~~ /              '~~)| |   '   / |    |   ~\
                \(   \    `-._ /~)_/|  /            '       | |      /  \     \_\  `\
                ,~`\  `-._  / )#####|/            '   /     | |  _--~ _/ | .-~~____--'
               ,'\  `-._  ~)~~ `                '           | | ((~>/~   \ (((' -_
             ,'   `-.___)~~      `                          | |           ~-_     ~\_
         _.,'        ,'           `                         | |            _-~-__    (
        |  `-.     ,'              `                \       | |          ((.-~~~-~_--~
        `\    `-.;'                  `#####""                | |           ""     ((.-~~
          `-._   )               \     |   |        .       |  \                 ""
              `~~  _/                  |    \               |   `---------------------
                |/~                `.  |     \        .     |  O    __.---------------
                 |                   \ ;      \             |   _.-~
                 |                    |        |            |  /  |
                  |                   |         |           |/'  |
        ");
        Console.ReadKey();

    }

    public static void AfterLevel3()
    {
        Narrator("After defeating Alucard, ");
        Narrator("the prince learns the truth:\r\n");
        Console.ReadKey();
        Narrator("Queen Lyra possesses ancient light magic, ");
        Narrator("A rare power capable of either saving or destroying the world. ");
        Narrator("............... ");
        Narrator("Maldrath plans to steal that power. ");
        Console.ReadKey();
        Narrator("Aric becomes more dedicated to learn the truth ");
        Narrator(" ");
        Narrator(" ");
        Narrator("The prince finally reaches the dark stronghold known as the Shadow Fortress ");
        Narrator("The fortress is surrounded by dark clouds and corrupted magic. ");
        Console.WriteLine(@" 
                           (   .                   _ _ _ _ _
            (   .     .  .=##                      ]-I-I-I-[                    /
          .=##   .  (      ( .                     \ `  ' /        \\\' ,      / //
            ( .   .=##  .       .                   |'  []          \\\//    _/ //'
          .     .   ( .    .        _----|          |.  '|           \_-//' /  //<'
                                     ----|_----|    | ' .|             \ ///  <//'
            _ _ _ _ _ _      _----|      | ----|    | [] |             /  >>   \\\`
            ]-I-I-I-I-[       ----|      |     |    |. ` |            /,)-^>>  _\`
             \ `   '_/            |     / \    |    | /^\|            (/   \\ / \\\
              []  `__|            ^    / ^ \   ^    | |*||                  //  //\\\
              |__   ,|           / \  / ^ ^`\ / \   | ===|                 ((`
           ___| ___ ,|__        / ^  /=_=_=_=\ ^ \  |, `_|
           I_I__I_I__I_I       (====(_________)_^___|____|____
           \-\--|-|--/-/       |     I  [ ]__I I_I__|____I_I_|
            |[] `    '|__   _  |_   _|`__  ._[  _-\--|-|--/-/
           / \  [] ` .|  |-| |-| |-| |_| |_| |_| | []   [] |
          <===>    `  |.            .      .     |    '    |
          ] []|  `    |   []    --   []      `   |   [] '  |
          <===>.  `   |  .   '  .       '  .[]   | '       |
           \_/    .   |       .       '          |   `  [] |
            | []    . |   .  .           ,  .    | ,    .  |
            |    . '  |       . []  '            |    []'  |
           / \   ..   |  `      .    .     `[]   | -   `   |
          <===>      .|=-=-=-=-=-=-=-=-=-=-=-=-=-|    .   / \
          ] []|` ` [] |`  .  .   _________   .   |-      <===>
          <===>  `  ' | '   |||  |       |  |||  |  []   <===>
           \_/     -- |   . |||  |       |  |||  | .  '   \_/
          ./|' . . . .|. . .||||/|_______|\|||| /|. . . . .|\_");
        Console.ReadKey();

        Narrator("Inside the throne hall,");
        Narrator(" Prince Aric discovers Queen Lyra trapped inside a magical barrier.  ");
        Console.WriteLine(@" 
         ||                  __                  ||
         ||                 /__`.                ||
         ||                / \ `\\               ||
         ||               /   \  `\              ||
         ||              /     \   \             ||
         ||             /_______\  /\            ||
         ||             (((( ))))                ||
         ||            (((' . ')))               ||
         ||            (((\_-_/)))               ||
         ||           ((((_) (_))))              ||
         ||           /((( \ / )))\              ||
         ||          / (((  ^  ))) \             ||
         ||         / / ((  ^  )) \ \            ||
         ||        ( (   \  ^  /   ) )           ||
         ||         \ \   )www(   / /            ||
         ||          `\\ /     \ //'             ||
         ||            /'       `\               ||
         ||           /           \              ||
         ||          /             \             ||
         ||         /               \            ||
         ||        /                 \           ||
         ||       /                   \          ||
         ||      /                     \         ||
         ||     /                       \        ||
         ||    /                         \       ||
         ||   /                           \      ||
         ||  |                             |     ||
         ||   `-----......_____......-----'      ||");
        Console.ReadKey();

        Narrator("Before he can free her,");

        Narrator("the dark sorcerer Maldrath appears.\r\n ");
        Console.ReadKey();

        Console.WriteLine(@" 
                                        ==(W{==========-      /===-                        
                                          ||  (.--.)         /===-_---~~~~~~~~~------____  
                                          | \_,|**|,__      |===-~___                _,-' `
                             -==\\        `\ ' `--'   ),    `//~\\   ~~~~`---.___.-~~      
                         ______-==|        /`\_. .__/\ \    | |  \\           _-~`         
                   __--~~~  ,-/-==\\      (   | .  |~~~~|   | |   `\        ,'             
                _-~       /'    |  \\     )__/==0==-\<>/   / /      \      /               
              .'        /       |   \\      /~\___/~~\/  /' /        \   /'                
             /  ____  /         |    \`\.__/-~~   \  |_/'  /          \/'                  
            /-'~    ~~~~~---__  |     ~-/~         ( )   /'        _--~`                   
                              \_|      /        _) | ;  ),   __--~~                        
                                '~~--_/      _-~/- |/ \   '-~ \                            
                               {\__--_/}    / \\_>-|)<__\      \                           
                               /'   (_/  _-~  | |__>--<__|      |                          
                              |   _/) )-~     | |__>--<__|      |                          
                              / /~ ,_/       / /__>---<__/      |                          
                             o-o _//        /-~_>---<__-~      /                           
                             (^(~          /~_>---<__-      _-~                            
                            ,/|           /__>--<__/     _-~                               
                         ,//('(          |__>--<__|     /  -               .----_          
                        ( ( '))          |__>--<__|    |                 /' _---_~\        
                     `-)) )) (           |__>--<__|    |               /'  /     ~\`\      
                    ,/,'//( (             \__>--<__\    \            /'  //        ||      
                  ,( ( ((, ))              ~-__>--<_~-_  ~--____---~' _/'/        /'       
                `~/  )` ) ,/|                 ~-_~>--<_/-__       __-~ _/                  
              ._-~//( )/ )) `                    ~~-'_/_/ /~~~~~~~__--~                    
               ;'( ')/ ,)(                              ~~~~~~~~~~                         
              ' ') '( (/                                                                   
                '   '  `");
        Console.ReadKey();

        Narrator("Maldrath reveals that he kidnapped the queen because she possesses ancient light magic, ");
        Narrator("a power he plans to use to strengthen his dark abilities and take revenge on the kingdom.");
        Narrator(" Maldrath:\r\n ");
        Narrator("  “You have come far, prince…");
        Narrator("  but this is where your journey ends!” ");
        Narrator(" The prince refuses to give up");
        Narrator(" he challenges Maldrath to a final battle.\r\n");

        Console.WriteLine(@" ");
    }

    public static void FinalScene()
    {
        Narrator("After the battle, ");
        Narrator("Maldrath collapses as the dark magic surrounding the fortress begins to fade.");
        Console.ReadKey();
        Narrator("Even in defeat,");
        Narrator("he warns that darkness will always exist in the world.\r\n");
        Narrator("Maldrath (weakly): ");
        Narrator(" “Even if I fall… darkness will rise again…”\r\n ");
        Console.ReadKey();
        Narrator("With Maldrath defeated,");
        Narrator(" the magical barrier holding the queen breaks apart. ");
        Narrator("The prince frees her at last.");
        Console.WriteLine(@" 
                            (,);        ...
                           ((  ^_.     (()))
                            ' / /_\    {' ())
                              L( '}     )_ (()
                               ) (_    (   (_)
                             (_  / }{)===='_/
                             | `/\/\     |   \
                             L___/ |     |    |
                              |  . \     |    |
                              |_/ \_\    |    |
                              ( ____ )   |    |
                               | | | |   |    |
                           ( --' | \ |_  '~~~~'
                           /_/---) (___)  _/Y");
        Console.ReadKey();
        Narrator("Queen Lyra:\r\n ");
        Narrator(" “You came for me…");
        Narrator("I knew you would.”\r\n ");
        Narrator("Prince Aric:\r\n");
        Narrator(" “I made a promise to bring you home.”\r\n ");
        Console.ReadKey();
        Narrator("The darkness surrounding the fortress slowly disappears as the sky begins to clear.\r\n ");
        Console.WriteLine(@" 
          ........::::::::::::..           .......|...............::::::::........
             .:::::;;;;;;;;;;;:::::.... .     \   | ../....::::;;;;:::::.......
                 .       ...........   / \\_   \  |  /     ......  .     ........./\
        ...:::../\\_  ......     ..._/'   \\\_  \###/   /\_    .../ \_.......   _//
        .::::./   \\\ _   .../\    /'      \\\\#######//   \/\   //   \_   ....////
            _/      \\\\   _/ \\\ /  x       \\\\###////      \////     \__  _/////
          ./   x       \\\/     \/ x X           \//////                   \/////
         /     XxX     \\/         XxX X                                    ////   x
        -----XxX-------------|-------XxX-----------*--------|---*-----|------------X--
               X        _X      *    X      **         **             x   **    *  X
              _X                    _X           x                *          x     X_");
        Console.ReadKey();

        Narrator("With the queen rescued and the dark sorcerer defeated,");
        Narrator("peace begins to return to the kingdom of Eldoria.\r\n");
        Console.ReadKey();

        Narrator("The prince and the queen leave the Shadow Fortress together,");
        Narrator("knowing that the kingdom is safe once again.\r\n");
        Console.WriteLine(@" 
              .                         .                             .
                              .     *          .
                        .
                                       .
           *              .                                    .
                                              .           '`                   .
              .                                         '`
                          //////                                     ####
                          //////                                   ########
                         (//////)                                 ##########
        ... ..  ..  ... . ////// . ... .())((() .. . . . .... . . . ..   . . ...  . . .
                     _____|____|_____  ((()(()))                   / / / / /
                    /                \ (((((((()                     /  / /
                   |                  |()(()(())_
                   |                  |(((()()() |
                   \                  '\()))))(  |
                    \                   \(((((  /
                     \              .    \___. (
                      \            / \    |:| ) \
                      |            | |`------'   |
                      \____________/  \_________/                             
         ..........................................................................
        ");
        Console.ReadKey();
        Narrator("The prince’s courage and determination have saved the kingdom");
        Narrator(" and proven that he is worthy of becoming its future ruler.\r\n");
        Console.ReadKey();
        Console.WriteLine(@" 
        ,---------. .---.  .---.     .-''-.              .-''-.  ,---.   .--. ______      
        \          \|   |  |_ _|   .'_ _   \           .'_ _   \ |    \  |  ||    _ `''.  
         `--.  ,---'|   |  ( ' )  / ( ` )   '         / ( ` )   '|  ,  \ |  || _ | ) _  \ 
            |   \   |   '-(_{;}_). (_ o _)  |        . (_ o _)  ||  |\_ \|  ||( ''_'  ) | 
            :_ _:   |      (_,_) |  (_,_)___|        |  (_,_)___||  _( )_\  || . (_) `. | 
            (_I_)   | _ _--.   | '  \   .---.        '  \   .---.| (_ o _)  ||(_    ._) ' 
           (_(=)_)  |( ' ) |   |  \  `-'    /         \  `-'    /|  (_,_)\  ||  (_.\.' /  
            (_I_)   (_{;}_)|   |   \       /           \       / |  |    |  ||       .'   
            '---'   '(_,_) '---'    `'-..-'             `'-..-'  '--'    '--''-----'`     
                                                                                          ");
        Console.ReadKey();
    }

    //Method to assign respective afterlevel story based on Main Game Loop Iteration
    public static void AfterLevel(int i)
    {
        if (i == 0)
        {
            AfterLevel1();
        }
        else if (i == 1)
        {
            AfterLevel2();
        }
        else if (i == 2)
        {
            AfterLevel3();
        }
        else
        {
            FinalScene();
        }
    }
}

}
