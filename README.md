# SLRSoftwareV2

This is a C# based project that I use to enter data pertaining to a set of parameters while reviewing scientific literature related to my PhD Thesis. 
The main motive behind saving this project Git is to save my work and data. 

The steps 1 and 2 in using this code

1. MySql Database need to be created with name "slrv1". Creating Database essentially means that creating Tables and priliminary data.a .txt file called SLRDatabase.txt in this folder has SQL commands to do this. 

2. After creating the Database building and running this project enables to enter review data. 

Future Work: 
I need to create a new project which reads the excel sheet and create database. Also the project should create C# classes and WinForms that allows to enter data pertaining to the parameters. Since I did not do this this project is only useful to me. 

1 In order to use it generally a Theme has to be defined. Theme genrally means pikcing parameters related to Research Questions (RQ). In my case I have the below Research Questions:

        RQ1
            Why is blockchain used as a data storage platform for software systems? 
            What factors influence designers and architects to incorporate blockchain?
            What application areas are mainly using blockchain?

        RQ2
            How existing or new software system architectures accommodate inherent characteristics of blockchain ? 
            How do the existing architectures like Client/Server or SOA/Microservices or Software Ecosystems (SECO) etc. are accommodating blockchain features like below:
                  Consensus
                  Participation
                  Node failures (BFT)
                  GAS
            How do the existing architectures accommodate the Blockchain network in the proposed software system? 
            Public, private or consortium? 
            Are there any new design or architecture patterns invented because of accommodating Blockchain?
            Are systems using out-of-the-box solutions for Blockchain or creating/tweaking new Blockchain technology? 

        RQ3
            What data is stored in blockchain? 
            Data format → Is the data structured or unstructured?
            Data store → Physical data store or Blockchain? 

        RQ4
            How are the below data related features realised?
            Data Model → 
            Data Integrity →  avoid duplication/redundant data.
            Data Access → any query languages/ APIs? 
            Data Indexing → 
            Relations in Data → 
            Data Sharding → data partition
            Data Provenance → inputs, entities, systems, processes that influence data
            Data Ownership → claims regarding which piece of data
            Data Authorization → explicit permission to use data
            Data Lineage → data's origins and where it moves over time

        RQ5
            How are distributed system characteristics used? 
            Synchronized/non-synchronized network (clock synchronization)
            Data replication strategy			
            Network topology

        RQ6
            How are the features that affect business used/modified?
            Scalability
            Consistency
            Read/Write latency

2 For each RQ I created a set of parameters which are listed in the Excel sheet called Search_Strings_RQ.xls document. 
