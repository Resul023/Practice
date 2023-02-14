Select * from AccountDb
--Show all email profile and profile account
EXECUTE msdb.dbo.sysmail_help_profile_sp; 
EXEC msdb.dbo.sysmail_help_profileaccount_sp;

EXEC msdb.dbo.sysmail_delete_profile_sp
@profile_id = 6  

EXEC msdb.dbo.sysmail_delete_profileaccount_sp
@profile_id = 8  


DECLARE @EmailRecipients VARCHAR(MAX);
SELECT  @EmailRecipients = COALESCE(@EmailRecipients + ';', '') + email FROM AccountDb
EXEC msdb.dbo.sp_send_dbmail
    @profile_name = 'ProfileP'
   ,@recipients = @EmailRecipients
   --,@blind_copy_recipients = '73m6u7h@code.edu.az'
   ,@subject = 'Email from SQL Server'
   --,@body = 'This is my First Email sent from SQL Server :)'


  ,@copy_recipients = '73m6u7h@code.edu.az'                        --Kim terefinden gelip gostermek
  --,@blind_copy_recipients =           
  --,@from_address =                  
  --,@reply_to =                      
  ,@body = 'Hello World'                                             
  ,@body_format = 'TEXT'                       --Html,txt ve.s
  --,@importance =                    
  --,@sensitivity =                   
  --,@file_attachments =                       --File url      
  ,@query =  'select * from AccountDb;'                       
  ,@execute_query_database ='EmailProfile'     
  ,@attach_query_result_as_file = 1            --File weklinde gondermek
  --,@query_attachment_filename =              
  --,@query_result_header =                    --Table name ustde yazilsin ya yox
  --,@query_result_width =            
  --,@query_result_separator =        
  --,@exclude_query_output =          
  --,@append_query_error =            
  --,@query_no_truncate =             
  --,@query_result_no_padding =       
  -- @mailitem_id =   
  

  --Check Mail Status
  SELECT * FROM msdb.dbo.sysmail_sentitems
  SELECT * FROM msdb.dbo.sysmail_faileditems  
  SELECT * FROM msdb.dbo.sysmail_unsentitems 






