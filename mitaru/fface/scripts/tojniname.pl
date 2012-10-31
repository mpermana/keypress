while (<>)
{
    $_ =~ /(\w*) (\w*) (.*)/;  
    print "$1 $2 Java_Taru_$3\n";

}
