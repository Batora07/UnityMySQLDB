# UnityMySQLDB
simple Project w/ login to sql db in Unity
~~~~
[To setup a test server]
Create a database in localhost, user = 'root', password = '' and 
add a new database named 'unityaccess'
with a new table named 'players' with 5 rows : 'id', 'username', 'hash', 'salt' and 'score'

Set the id table as Primary key and auto increment.
set the username table as Unique

~~~~
The php server code is also available in this repository.