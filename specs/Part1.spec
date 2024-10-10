# 1. API Testing

In C#, develop a suite of automated API tests against API end points from https://jsonplaceholder.typicode.com/guide. 

Endpoints to cover:
-  GET https://jsonplaceholder.typicode.com/posts
-  POST https://jsonplaceholder.typicode.com/posts
-  PUT https://jsonplaceholder.typicode.com/posts/{postId}
-  DELETE https://jsonplaceholder.typicode.com/posts/{postId}
-  POST https://jsonplaceholder.typicode.com/posts/{postId}/comments
-  GET https://jsonplaceholder.typicode.com/comments?postId={postId}

* Initialize connection to "https://jsonplaceholder.typicode.com"

## 1. GET Posts

|requestName      |endPoint  |expectedStatus|
|-----------------|----------|--------------|
|allPostsHappyPath|/posts    |200           |
|onePostHappyPath |/posts/1  |200           |
|outOfRange       |/posts/101|404           |
|invalidEndpoint  |/posts/yes|404           |

* Send request <requestName> to "Get" <endPoint>
* Verify status code of <requestName> is <expectedStatus>
* Print response content of <requestName>

## 2. POST Posts

|caseInput    |status    |
|-------------|----------|
|HappyPath    |201       |
|MissingTitle |201       |
|ExtraInfo    |201       |
|WrongInfo    |201       |
|InvalidValues|201       |
|EmptyBody    |201       |

* Set <caseInput> in "Part1.2.json" as "object" to "CreateResourceBody"
* Send request "CreateTest1" to "Post" "/posts" with "CreateResourceBody" as body
* Verify status code of "CreateTest1" is <status>
* Print response content of "CreateTest1"

## 2.5. POST Posts Issue

It seems like the server does not update after creating the new Post

* Set "{ \"title\":\"Vinay\", \"body\":\"Jacob-John\", \"userId\":1989}" as "object" to "CreateResourceBody"
* Send request "CreateTest1" to "Post" "/posts" with "CreateResourceBody" as body
* Verify status code of "CreateTest1" is "201"
* Print response content of "CreateTest1"
* Set response content of "CreateTest1" as "object" to "CreateTest1Resp"
* Set "id" in "CreateTest1Resp" as "String" to "id"
* Send request "CheckNewResource" to "Get" "/posts/{id}"
* Print response content of "CheckNewResource"
* Verify status code of "CheckNewResource" is "404"

## 3. Put /post/{postId}

|caseInput    |endPoint  |status    |
|-------------|----------|----------|
|HappyPath1   |/posts/1  |200       |
|HappyPath100 |/posts/100|200       |
|HappyPath100 |/posts/1  |200       |
|HappyPath1   |/posts/101|500       |
|HappyPath1   |/posts/yes|500       |
|MissingTitle |/posts/1  |200       |
|ExtraInfo    |/posts/1  |200       |
|WrongInfo    |/posts/1  |200       |
|InvalidValues|/posts/1  |200       |
|EmptyBody    |/posts/1  |200       |

* Set <caseInput> in "Part1.3.json" as "object" to "CreateResourceBody"
* Send request "CreateTest1" to "Put" <endPoint> with "CreateResourceBody" as body
* Print response content of "CreateTest1"
* Verify status code of "CreateTest1" is <status>

## 4. DELETE /post/{postId}

|endPoint  |expectedStatus|
|----------|--------------|
|/posts    |404           |
|/posts/1  |200           |
|/posts/100|200           |
|/posts/101|200           |
|/posts/yes|200           |

* Send request "DeleteRequest" to "Delete" <endPoint>
* Print response content of "DeleteRequest"
* Verify status code of "DeleteRequest" is <expectedStatus>

## 5. POST /posts/{postId}/comments

|caseInput    |endPoint           |status    |
|-------------|-------------------|----------|
|HappyPath1   |/posts/1/comments  |201       |
|HappyPath100 |/posts/100/comments|201       |
|HappyPath100 |/posts/1/comments  |201       |
|HappyPath1   |/posts/101/comments|201       |
|HappyPath1   |/posts/yes/comments|201       |
|MissingInfo  |/posts/1/comments  |201       |
|ExtraInfo    |/posts/1/comments  |201       |
|WrongInfo    |/posts/1/comments  |201       |
|InvalidValues|/posts/1/comments  |201       |
|EmptyBody    |/posts/1/comments  |201       |

* Set <caseInput> in "Part1.5.json" as "object" to "PostCommentBody"
* Send request "TestComment" to "Post" <endPoint> with "PostCommentBody" as body
* Print response content of "TestComment"
* Verify status code of "TestComment" is <status>

## 6. GET /comments?postId={postId}

|params         |expectedStatus|
|---------------|--------------|
|{"postId":1}   |200           |
|{"postId":100} |200           |
|{"postId":101} |404           |
|{"postId":true}|404           |
|{"foo":"bar"}  |404           |

* Set <params> as "object" to "ParamObject"
* Send request "GetComments" to "Get" "/comments" with "ParamObject" as parameters
* Print response content of "GetComments"
* Verify status code of "GetComments" is <expectedStatus>
