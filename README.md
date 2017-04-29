# EmailService
Email sending with providers

Create a program that accepts the necessary information and sends emails. It should provide an abstraction between two different email service providers. If one of the services goes down, your service can quickly failover to a different provider without affecting your customers. Example Email Providers:

SendGrid
Mailgun
Amazon SES
Requirements

Your program must accept input from two sources: a filename passed in command line arguments and STDIN. For example, on Linux or OSX both './email input.txt' and './myprogram < input.txt' should work.
Your program must accept at a minimum 4 space delimited arguments: a) from b) to c) subject d) text
When all input has been read and processed, a summary should be generated and written to STDOUT.
The summary should include the email of each sender, followed by # of emails sent and service provider. If there were multiple service providers (in case of failure from the first one), you can print a comma separated list of providers used.
# Example Input:

jlisam@insikt.com xyz@gmail.com "work work" "hello world"
hello@world.com hello@insikt.com "subject" "text"
jlisam@insikt.com john@gmail.com "nice" "No way"

# Example Output

jlisam@insikt.com 2 SendGrid,MailGun
hello@world 1 SendGrid

Implement your solution in any programming language you wish. Please write automated tests and include them with your submission.

In addition to your code and tests, provide instructions on what we need to do to build and run your program locally on a standard unix-like development environment (e.g. Mac laptop or Linux server).
