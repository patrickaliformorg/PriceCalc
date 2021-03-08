## Rough ideas behind PriceCalc

Discount classes - deal with the content of the discount, with the intention that these could be reused for other offers
Offer classes - deal with the application of the discounts, they accept all the products in the basket and based on the logic, apply the number of discounts applicable (code in these classes needs 'cleaning' a little)

### What didn't I get done

Logging, I would like to have more logging of the details of the offers being added and the discounts applied to build up a narrative, my TestCase structure
of the tests doesn't make this simple to start adding expect logs assertions but given more time this is what I would look at next

### Excuses

Some of the code may not be optimally formatted my usual setting are something I don't have set up and locally and haven't installed ReSharper yet

Loosely typed Products - the Products are just identified by there name (string), Ids would be neccessary in the real world

I've paid almost no attention to the private/public/internal/protected status of methods or fields
