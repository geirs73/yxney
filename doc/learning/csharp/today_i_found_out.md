# Today I found out

## 2024-01-24 Fire and forget with channel based queuing and background service

Source:
<https://learn.microsoft.com/en-us/dotnet/core/extensions/queue-service>

If you do something dumb like firing of some analysis that you don't care
whether succeeds or fails, and you want control of how often you do it given an
situation that occurs, you can use bounded channels and set the capacity limit
quite low. I do this for some DNS checking when certain errors occur in a hosted
service.

To be able to have different behaviour on the different queues, you may want to
use generics, abstract and inheritance to create specific queues and background
processor services that handle different scenarios. Maybe also unbounded.
