## Demo CRUD Using OData using code first

Include 2 PHASE

- PHASE 1: Book Manage
- PHASE 2: Product Manage (Product 1-n Category)

---

clone project -> Set mutil project base on PHASE

test api sample for OData:

PHASE 1: http://localhost:5001/odata/Books?$filter=Title eq 'Enterprise Games'
    eq - equals to.
    ne - not equals to
    gt - greater than
    ge - greater than or equal
    lt - less than
    le - less than or equal
http://localhost:5001/odata/Books?$select=Title,Author
http://localhost:50246/odata/Presses?$orderby=Name desc
http://localhost:50246/odata/Presses?$top=2&$skip=1

PHASE 2: https://localhost:5001/odata/Products?$select=Id,Name
