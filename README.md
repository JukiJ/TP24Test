# TP24Test

## Running the application
 - docker-compose up

## Remarks
 - Method to get receivables summary is doing few trips to the database but I believe that is less of an issue that if it was pulling all data in memory and then doing the calculations. Much better solution would be to create a view in the database or a stored procedure if needed. I could have also wrote raw sql or similar but I think for the purpose of the task this can be enough. Reading the task and thinking about the use case it seems like this would be good enough approach. Also I was thinking to associate maybe every batch of this receivables with a specific company that would send them but since the task said keep the payload as is I left it like that.

 - I spent about 4-5 hours on the assignment
