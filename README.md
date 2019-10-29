# MuranoTestApp

Реализованы две версии поисковика: **Lazy** и **Default**. В Startup выбран **Default**, но при необходимости можно поменять на **Lazy**,
поменяв **_services.AddSearchService()_** на **_AddLazySearchService()_**. 

-- **Lazy** поисковик выполняет запросы на внешниe поисковики только при отсутствии в базе заданного запроса.

Для инициализирования бд нужно выполнить команду **_update-database_**, выбрав стартовым проектом **MuranoTestApp**.

Чтобы добавить новый поисковик, нужно реализовать ISearcher и добавить его как **Scoped** в DI в методе **_ConfigureServices()_** 
в классе Startup.

Для функционирования нужны файлы конфигурации **appsettings.json** (DefaultConnection бд) и **SearchersConfig.json** (конфигурации для
добавленных поисковиков). Их нужно поместить в директорию проекта **MuranoTestApp**.
