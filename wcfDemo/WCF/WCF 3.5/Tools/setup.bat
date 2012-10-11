makecert -sr LocalMachine -ss My -a sha1 -n CN=Webabcd -sky exchange -pe
certmgr -add -r LocalMachine -s My -c -n Webabcd -s TrustedPeople