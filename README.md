Just another tool for simplify work at my current job.

First opinion was:
   1. Input from user
   2. Querry to db
   3. Save to file
   4. Repeat

After some try, i figure out, querring db with each input was in my current environment too slow, about 14 second per querry, 
so now i am loading all necessery data by one querry to memory, it cost more memory, but much more faster. / working, but need to repair cost of memory, after some refactoring 1.3Milion rows cost from 500MB, 250 was acceptable for now

Next step was add point between 3. and 4. automatic open file and wait for close. / working

Last thing was add cancellation for querry method, i used Task class where runnig loop for checking if user pressed keyboard. / not working properlly, method Console.ReadKey() still stealing letter from input
