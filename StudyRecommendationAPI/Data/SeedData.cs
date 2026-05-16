using StudyRecommendationAPI.Models;

namespace StudyRecommendationAPI.Data;

public static class SeedData
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (db.Subjects.Any()) return;

        DateTime now = DateTime.UtcNow;

        // ── MATERIA 1: INGENIERÍA DE DATOS II ──────────────────────────────
        Subject subject1 = new()
        {
            Name = "Ingeniería de Datos II",
            Career = "Ingeniería en Sistemas",
            Institution = "UADE - Facultad de Ingeniería (FAIN)",
            CreatedAt = now
        };
        db.Subjects.Add(subject1);
        await db.SaveChangesAsync();

        List<Topic> topics1 = new()
        {
            new Topic { SubjectId = subject1.Id, UnitNumber = 1, UnitName = "Introducción a NoSQL", TopicName = "Concepto de NoSQL", OrderIndex = 1 },
            new Topic { SubjectId = subject1.Id, UnitNumber = 1, UnitName = "Introducción a NoSQL", TopicName = "Modelo Relacional vs estructuras No Relacionales", OrderIndex = 2 },
            new Topic { SubjectId = subject1.Id, UnitNumber = 1, UnitName = "Introducción a NoSQL", TopicName = "Criterio de selección entre ambos modelos", OrderIndex = 3 },
            new Topic { SubjectId = subject1.Id, UnitNumber = 1, UnitName = "Introducción a NoSQL", TopicName = "Relación con volúmenes de datos y de consultas", OrderIndex = 4 },
            new Topic { SubjectId = subject1.Id, UnitNumber = 1, UnitName = "Introducción a NoSQL", TopicName = "ACID en NoSQL", OrderIndex = 5 },

            new Topic { SubjectId = subject1.Id, UnitNumber = 2, UnitName = "Modelos NoSQL", TopicName = "Distintos modelos NoSQL (clave-valor, documentos, grafos, columnares)", OrderIndex = 1 },
            new Topic { SubjectId = subject1.Id, UnitNumber = 2, UnitName = "Modelos NoSQL", TopicName = "Implementación y funcionamiento de cada modelo", OrderIndex = 2 },
            new Topic { SubjectId = subject1.Id, UnitNumber = 2, UnitName = "Modelos NoSQL", TopicName = "Comparación de las distintas soluciones", OrderIndex = 3 },

            new Topic { SubjectId = subject1.Id, UnitNumber = 3, UnitName = "Administración de datos no estructurados", TopicName = "Administración y recuperación desde fuentes de datos no estructurados", OrderIndex = 1 },
            new Topic { SubjectId = subject1.Id, UnitNumber = 3, UnitName = "Administración de datos no estructurados", TopicName = "Interfaces de administración", OrderIndex = 2 },
            new Topic { SubjectId = subject1.Id, UnitNumber = 3, UnitName = "Administración de datos no estructurados", TopicName = "Técnicas de acceso", OrderIndex = 3 },
            new Topic { SubjectId = subject1.Id, UnitNumber = 3, UnitName = "Administración de datos no estructurados", TopicName = "Distribución de datos", OrderIndex = 4 },
            new Topic { SubjectId = subject1.Id, UnitNumber = 3, UnitName = "Administración de datos no estructurados", TopicName = "Escalamiento horizontal", OrderIndex = 5 },

            new Topic { SubjectId = subject1.Id, UnitNumber = 4, UnitName = "Replicación y particionamiento", TopicName = "Teorema CAP", OrderIndex = 1 },
            new Topic { SubjectId = subject1.Id, UnitNumber = 4, UnitName = "Replicación y particionamiento", TopicName = "Modelos de replicación (Master-Slave, Master-Slave Master, peer to peer)", OrderIndex = 2 },
            new Topic { SubjectId = subject1.Id, UnitNumber = 4, UnitName = "Replicación y particionamiento", TopicName = "Criterios de particionamiento", OrderIndex = 3 },
            new Topic { SubjectId = subject1.Id, UnitNumber = 4, UnitName = "Replicación y particionamiento", TopicName = "Tipos de consistencia (eventual, por quorum, plena de escritura, plena de lectura)", OrderIndex = 4 },

            new Topic { SubjectId = subject1.Id, UnitNumber = 5, UnitName = "Acceso desde aplicaciones", TopicName = "Acceso a estructura NoSQL desde una aplicación", OrderIndex = 1 },
            new Topic { SubjectId = subject1.Id, UnitNumber = 5, UnitName = "Acceso desde aplicaciones", TopicName = "Herramientas de conectividad", OrderIndex = 2 },
            new Topic { SubjectId = subject1.Id, UnitNumber = 5, UnitName = "Acceso desde aplicaciones", TopicName = "Evaluación de resultados", OrderIndex = 3 },

            new Topic { SubjectId = subject1.Id, UnitNumber = 6, UnitName = "Grandes volúmenes de datos", TopicName = "Manejo de grandes volúmenes de datos", OrderIndex = 1 },
            new Topic { SubjectId = subject1.Id, UnitNumber = 6, UnitName = "Grandes volúmenes de datos", TopicName = "Integración de estructuras NoSQL en Data Marts", OrderIndex = 2 },
            new Topic { SubjectId = subject1.Id, UnitNumber = 6, UnitName = "Grandes volúmenes de datos", TopicName = "Comparación de rendimientos con estructuras relacionales", OrderIndex = 3 },
        };
        db.Topics.AddRange(topics1);
        await db.SaveChangesAsync();

        // ── MATERIA 2: SEMINARIO DE INTEGRACIÓN PROFESIONAL ────────────────
        Subject subject2 = new()
        {
            Name = "Seminario de Integración Profesional",
            Career = "Ingeniería en Sistemas",
            Institution = "UADE - Facultad de Ingeniería (FAIN)",
            CreatedAt = now
        };
        db.Subjects.Add(subject2);
        await db.SaveChangesAsync();

        List<Topic> topics2 = new()
        {
            new Topic { SubjectId = subject2.Id, UnitNumber = 1, UnitName = "Definición de problema y descubrimiento", TopicName = "Definición de problema", OrderIndex = 1 },
            new Topic { SubjectId = subject2.Id, UnitNumber = 1, UnitName = "Definición de problema y descubrimiento", TopicName = "Concepto de MVP", OrderIndex = 2 },
            new Topic { SubjectId = subject2.Id, UnitNumber = 1, UnitName = "Definición de problema y descubrimiento", TopicName = "Metodología de trabajo", OrderIndex = 3 },
            new Topic { SubjectId = subject2.Id, UnitNumber = 1, UnitName = "Definición de problema y descubrimiento", TopicName = "Descubrimiento de desafíos", OrderIndex = 4 },

            new Topic { SubjectId = subject2.Id, UnitNumber = 2, UnitName = "Investigación y análisis", TopicName = "Narrativa de problemas", OrderIndex = 1 },
            new Topic { SubjectId = subject2.Id, UnitNumber = 2, UnitName = "Investigación y análisis", TopicName = "Métodos de descubrimiento (encuesta, entrevista, observación)", OrderIndex = 2 },
            new Topic { SubjectId = subject2.Id, UnitNumber = 2, UnitName = "Investigación y análisis", TopicName = "Segmentación y target", OrderIndex = 3 },
            new Topic { SubjectId = subject2.Id, UnitNumber = 2, UnitName = "Investigación y análisis", TopicName = "Análisis de mercado", OrderIndex = 4 },

            new Topic { SubjectId = subject2.Id, UnitNumber = 3, UnitName = "Design Thinking y experiencia de usuario", TopicName = "Design Thinking", OrderIndex = 1 },
            new Topic { SubjectId = subject2.Id, UnitNumber = 3, UnitName = "Design Thinking y experiencia de usuario", TopicName = "User Persona", OrderIndex = 2 },
            new Topic { SubjectId = subject2.Id, UnitNumber = 3, UnitName = "Design Thinking y experiencia de usuario", TopicName = "Mapa de empatía", OrderIndex = 3 },
            new Topic { SubjectId = subject2.Id, UnitNumber = 3, UnitName = "Design Thinking y experiencia de usuario", TopicName = "Escenario actual", OrderIndex = 4 },
            new Topic { SubjectId = subject2.Id, UnitNumber = 3, UnitName = "Design Thinking y experiencia de usuario", TopicName = "User Journey Map", OrderIndex = 5 },

            new Topic { SubjectId = subject2.Id, UnitNumber = 4, UnitName = "Ideación y estrategia", TopicName = "Focus Groups", OrderIndex = 1 },
            new Topic { SubjectId = subject2.Id, UnitNumber = 4, UnitName = "Ideación y estrategia", TopicName = "Ideación de soluciones y brainstorming", OrderIndex = 2 },
            new Topic { SubjectId = subject2.Id, UnitNumber = 4, UnitName = "Ideación y estrategia", TopicName = "Roadmap", OrderIndex = 3 },
            new Topic { SubjectId = subject2.Id, UnitNumber = 4, UnitName = "Ideación y estrategia", TopicName = "Estrategia de negocios digitales", OrderIndex = 4 },
            new Topic { SubjectId = subject2.Id, UnitNumber = 4, UnitName = "Ideación y estrategia", TopicName = "Narrativa de solución", OrderIndex = 5 },

            new Topic { SubjectId = subject2.Id, UnitNumber = 5, UnitName = "Historias de usuario y diseño", TopicName = "Historias de usuario y criterios de aceptación", OrderIndex = 1 },
            new Topic { SubjectId = subject2.Id, UnitNumber = 5, UnitName = "Historias de usuario y diseño", TopicName = "SPIDR", OrderIndex = 2 },
            new Topic { SubjectId = subject2.Id, UnitNumber = 5, UnitName = "Historias de usuario y diseño", TopicName = "User Story Map", OrderIndex = 3 },
            new Topic { SubjectId = subject2.Id, UnitNumber = 5, UnitName = "Historias de usuario y diseño", TopicName = "User flow y user task", OrderIndex = 4 },
            new Topic { SubjectId = subject2.Id, UnitNumber = 5, UnitName = "Historias de usuario y diseño", TopicName = "Taller de UX/UI", OrderIndex = 5 },

            new Topic { SubjectId = subject2.Id, UnitNumber = 6, UnitName = "Gestión ágil", TopicName = "Sprints y metodología Scrum", OrderIndex = 1 },
            new Topic { SubjectId = subject2.Id, UnitNumber = 6, UnitName = "Gestión ágil", TopicName = "Reviews y retrospectivas", OrderIndex = 2 },
            new Topic { SubjectId = subject2.Id, UnitNumber = 6, UnitName = "Gestión ágil", TopicName = "Feedback 360", OrderIndex = 3 },
            new Topic { SubjectId = subject2.Id, UnitNumber = 6, UnitName = "Gestión ágil", TopicName = "Pitch y oratoria", OrderIndex = 4 },
            new Topic { SubjectId = subject2.Id, UnitNumber = 6, UnitName = "Gestión ágil", TopicName = "Negociación", OrderIndex = 5 },
        };
        db.Topics.AddRange(topics2);
        await db.SaveChangesAsync();

        // ── MATERIA 3: ESTADÍSTICA APLICADA ───────────────────────────────
        Subject subject3 = new()
        {
            Name = "Estadística Aplicada",
            Career = "Ingeniería en Sistemas",
            Institution = "UADE - Facultad de Ingeniería (FAIN)",
            CreatedAt = now
        };
        db.Subjects.Add(subject3);
        await db.SaveChangesAsync();

        List<Topic> topics3 = new()
        {
            new Topic { SubjectId = subject3.Id, UnitNumber = 1, UnitName = "Estadística descriptiva", TopicName = "Medidas de tendencia central (media, mediana, moda)", OrderIndex = 1 },
            new Topic { SubjectId = subject3.Id, UnitNumber = 1, UnitName = "Estadística descriptiva", TopicName = "Medidas de dispersión (varianza, desvío estándar)", OrderIndex = 2 },
            new Topic { SubjectId = subject3.Id, UnitNumber = 1, UnitName = "Estadística descriptiva", TopicName = "Visualización de datos: histogramas y boxplots", OrderIndex = 3 },

            new Topic { SubjectId = subject3.Id, UnitNumber = 2, UnitName = "Probabilidad", TopicName = "Conceptos básicos de probabilidad", OrderIndex = 1 },
            new Topic { SubjectId = subject3.Id, UnitNumber = 2, UnitName = "Probabilidad", TopicName = "Probabilidad condicional e independencia", OrderIndex = 2 },
            new Topic { SubjectId = subject3.Id, UnitNumber = 2, UnitName = "Probabilidad", TopicName = "Teorema de Bayes", OrderIndex = 3 },
            new Topic { SubjectId = subject3.Id, UnitNumber = 2, UnitName = "Probabilidad", TopicName = "Variables aleatorias discretas y continuas", OrderIndex = 4 },

            new Topic { SubjectId = subject3.Id, UnitNumber = 3, UnitName = "Distribuciones de probabilidad", TopicName = "Distribución binomial y de Poisson", OrderIndex = 1 },
            new Topic { SubjectId = subject3.Id, UnitNumber = 3, UnitName = "Distribuciones de probabilidad", TopicName = "Distribución normal y teorema central del límite", OrderIndex = 2 },
            new Topic { SubjectId = subject3.Id, UnitNumber = 3, UnitName = "Distribuciones de probabilidad", TopicName = "Distribuciones t, chi-cuadrado y F", OrderIndex = 3 },

            new Topic { SubjectId = subject3.Id, UnitNumber = 4, UnitName = "Inferencia estadística", TopicName = "Estimación puntual e intervalos de confianza", OrderIndex = 1 },
            new Topic { SubjectId = subject3.Id, UnitNumber = 4, UnitName = "Inferencia estadística", TopicName = "Pruebas de hipótesis: errores tipo I y tipo II", OrderIndex = 2 },
            new Topic { SubjectId = subject3.Id, UnitNumber = 4, UnitName = "Inferencia estadística", TopicName = "Prueba t de Student y prueba chi-cuadrado", OrderIndex = 3 },
            new Topic { SubjectId = subject3.Id, UnitNumber = 4, UnitName = "Inferencia estadística", TopicName = "p-valor e interpretación de resultados", OrderIndex = 4 },

            new Topic { SubjectId = subject3.Id, UnitNumber = 5, UnitName = "Regresión y correlación", TopicName = "Correlación de Pearson y Spearman", OrderIndex = 1 },
            new Topic { SubjectId = subject3.Id, UnitNumber = 5, UnitName = "Regresión y correlación", TopicName = "Regresión lineal simple", OrderIndex = 2 },
            new Topic { SubjectId = subject3.Id, UnitNumber = 5, UnitName = "Regresión y correlación", TopicName = "Regresión lineal múltiple", OrderIndex = 3 },

            new Topic { SubjectId = subject3.Id, UnitNumber = 6, UnitName = "Series temporales", TopicName = "Componentes de una serie temporal (tendencia, estacionalidad, ruido)", OrderIndex = 1 },
            new Topic { SubjectId = subject3.Id, UnitNumber = 6, UnitName = "Series temporales", TopicName = "Modelos de suavizado exponencial", OrderIndex = 2 },
            new Topic { SubjectId = subject3.Id, UnitNumber = 6, UnitName = "Series temporales", TopicName = "Introducción a modelos ARIMA", OrderIndex = 3 },
        };
        db.Topics.AddRange(topics3);
        await db.SaveChangesAsync();

        // ── RECURSOS MOCKEADOS ─────────────────────────────────────────────
        // Obtenemos IDs por nombre para que el seed sea independiente del orden de inserción
        Dictionary<string, int> topicIds = db.Topics
            .Select(t => new { t.TopicName, t.Id })
            .ToDictionary(x => x.TopicName, x => x.Id);

        List<Resource> resources = new()
        {
            // Unidad 1 - Introducción a NoSQL
            new Resource { TopicId = topicIds["Concepto de NoSQL"], Type = "video", Title = "¿Qué es NoSQL? Introducción completa en español", Url = "https://www.youtube.com/watch?v=mock_nosql01", Source = "YouTube", PositiveVotes = 45, NegativeVotes = 5, CreatedAt = now },
            new Resource { TopicId = topicIds["Concepto de NoSQL"], Type = "article", Title = "NoSQL Distilled - Resumen conceptual", Url = "https://martinfowler.com/books/nosql.html", Source = "Martin Fowler", PositiveVotes = 30, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["Concepto de NoSQL"], Type = "video", Title = "Bases de datos NoSQL desde cero", Url = "https://www.youtube.com/watch?v=mock_nosql02", Source = "YouTube", PositiveVotes = 0, NegativeVotes = 0, CreatedAt = now },

            new Resource { TopicId = topicIds["Modelo Relacional vs estructuras No Relacionales"], Type = "video", Title = "SQL vs NoSQL: diferencias clave explicadas", Url = "https://www.youtube.com/watch?v=mock_sqlvnosql01", Source = "YouTube", PositiveVotes = 38, NegativeVotes = 4, CreatedAt = now },
            new Resource { TopicId = topicIds["Modelo Relacional vs estructuras No Relacionales"], Type = "article", Title = "Relational vs Non-Relational Databases", Url = "https://www.ibm.com/cloud/blog/relational-vs-non-relational-databases", Source = "IBM Cloud", PositiveVotes = 20, NegativeVotes = 8, CreatedAt = now },
            new Resource { TopicId = topicIds["Modelo Relacional vs estructuras No Relacionales"], Type = "book", Title = "Seven Databases in Seven Weeks - Cap. Intro", Url = "https://pragprog.com/titles/pwrdata/seven-databases-in-seven-weeks-2nd-edition/", Source = "Pragmatic Programmers", PositiveVotes = 12, NegativeVotes = 2, CreatedAt = now },

            new Resource { TopicId = topicIds["Criterio de selección entre ambos modelos"], Type = "article", Title = "¿Cuándo usar SQL y cuándo NoSQL?", Url = "https://www.digitalocean.com/community/tutorials/understanding-sql-and-nosql-databases", Source = "DigitalOcean", PositiveVotes = 25, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["Criterio de selección entre ambos modelos"], Type = "video", Title = "Elegir entre SQL y NoSQL: criterios prácticos", Url = "https://www.youtube.com/watch?v=mock_criterio01", Source = "YouTube", PositiveVotes = 18, NegativeVotes = 6, CreatedAt = now },

            new Resource { TopicId = topicIds["Relación con volúmenes de datos y de consultas"], Type = "article", Title = "Big Data y bases de datos NoSQL: la relación", Url = "https://www.mongodb.com/nosql-explained/nosql-vs-sql", Source = "MongoDB", PositiveVotes = 15, NegativeVotes = 2, CreatedAt = now },
            new Resource { TopicId = topicIds["Relación con volúmenes de datos y de consultas"], Type = "video", Title = "Escalabilidad y volumen de datos en NoSQL", Url = "https://www.youtube.com/watch?v=mock_volume01", Source = "YouTube", PositiveVotes = 0, NegativeVotes = 0, CreatedAt = now },

            new Resource { TopicId = topicIds["ACID en NoSQL"], Type = "article", Title = "ACID vs BASE: consistencia en bases de datos distribuidas", Url = "https://www.databricks.com/glossary/acid-transactions", Source = "Databricks", PositiveVotes = 33, NegativeVotes = 4, CreatedAt = now },
            new Resource { TopicId = topicIds["ACID en NoSQL"], Type = "video", Title = "Propiedades ACID explicadas con ejemplos", Url = "https://www.youtube.com/watch?v=mock_acid01", Source = "YouTube", PositiveVotes = 22, NegativeVotes = 5, CreatedAt = now },
            new Resource { TopicId = topicIds["ACID en NoSQL"], Type = "article", Title = "Transactions in NoSQL systems", Url = "https://db-engines.com/en/blog_post/79", Source = "DB-Engines", PositiveVotes = 8, NegativeVotes = 4, CreatedAt = now },

            // Unidad 2 - Modelos NoSQL
            new Resource { TopicId = topicIds["Distintos modelos NoSQL (clave-valor, documentos, grafos, columnares)"], Type = "video", Title = "Los 4 modelos NoSQL: clave-valor, documentos, grafos y columnar", Url = "https://www.youtube.com/watch?v=mock_modelos01", Source = "YouTube", PositiveVotes = 50, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["Distintos modelos NoSQL (clave-valor, documentos, grafos, columnares)"], Type = "article", Title = "Types of NoSQL Databases", Url = "https://www.mongodb.com/nosql-explained/types-of-nosql-databases", Source = "MongoDB", PositiveVotes = 28, NegativeVotes = 2, CreatedAt = now },
            new Resource { TopicId = topicIds["Distintos modelos NoSQL (clave-valor, documentos, grafos, columnares)"], Type = "book", Title = "NoSQL Distilled - Modelos de datos", Url = "https://martinfowler.com/books/nosql.html", Source = "Martin Fowler", PositiveVotes = 15, NegativeVotes = 5, CreatedAt = now },

            new Resource { TopicId = topicIds["Implementación y funcionamiento de cada modelo"], Type = "video", Title = "MongoDB, Redis, Cassandra y Neo4j: funcionamiento interno", Url = "https://www.youtube.com/watch?v=mock_impl01", Source = "YouTube", PositiveVotes = 40, NegativeVotes = 6, CreatedAt = now },
            new Resource { TopicId = topicIds["Implementación y funcionamiento de cada modelo"], Type = "article", Title = "Redis: cómo funciona internamente", Url = "https://redis.io/docs/about/", Source = "Redis Docs", PositiveVotes = 20, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["Implementación y funcionamiento de cada modelo"], Type = "video", Title = "Cassandra vs MongoDB vs Redis - Comparación interna", Url = "https://www.youtube.com/watch?v=mock_impl02", Source = "YouTube", PositiveVotes = 3, NegativeVotes = 1, CreatedAt = now },

            new Resource { TopicId = topicIds["Comparación de las distintas soluciones"], Type = "article", Title = "DB-Engines Ranking: comparativa NoSQL", Url = "https://db-engines.com/en/ranking", Source = "DB-Engines", PositiveVotes = 22, NegativeVotes = 4, CreatedAt = now },
            new Resource { TopicId = topicIds["Comparación de las distintas soluciones"], Type = "video", Title = "NoSQL showdown: comparando las principales soluciones", Url = "https://www.youtube.com/watch?v=mock_comp01", Source = "YouTube", PositiveVotes = 0, NegativeVotes = 0, CreatedAt = now },

            // Unidad 3 - Administración de datos no estructurados
            new Resource { TopicId = topicIds["Administración y recuperación desde fuentes de datos no estructurados"], Type = "video", Title = "Datos no estructurados: administración y recuperación", Url = "https://www.youtube.com/watch?v=mock_unstr01", Source = "YouTube", PositiveVotes = 18, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["Administración y recuperación desde fuentes de datos no estructurados"], Type = "article", Title = "Unstructured Data Management - Overview", Url = "https://ocw.mit.edu/courses/data-unstructured", Source = "MIT OCW", PositiveVotes = 12, NegativeVotes = 2, CreatedAt = now },

            new Resource { TopicId = topicIds["Interfaces de administración"], Type = "video", Title = "MongoDB Compass y Robo 3T: interfaces de administración NoSQL", Url = "https://www.youtube.com/watch?v=mock_iface01", Source = "YouTube", PositiveVotes = 30, NegativeVotes = 2, CreatedAt = now },
            new Resource { TopicId = topicIds["Interfaces de administración"], Type = "article", Title = "Herramientas de administración para bases NoSQL", Url = "https://www.digitalocean.com/community/tutorials/nosql-admin-tools", Source = "DigitalOcean", PositiveVotes = 14, NegativeVotes = 5, CreatedAt = now },

            new Resource { TopicId = topicIds["Técnicas de acceso"], Type = "video", Title = "Técnicas de acceso a datos en sistemas NoSQL", Url = "https://www.youtube.com/watch?v=mock_access01", Source = "YouTube", PositiveVotes = 20, NegativeVotes = 4, CreatedAt = now },
            new Resource { TopicId = topicIds["Técnicas de acceso"], Type = "article", Title = "Data Access Patterns in NoSQL", Url = "https://www.datastax.com/blog/nosql-data-access-patterns", Source = "DataStax", PositiveVotes = 9, NegativeVotes = 3, CreatedAt = now },

            new Resource { TopicId = topicIds["Distribución de datos"], Type = "video", Title = "Distribución y sharding en bases de datos NoSQL", Url = "https://www.youtube.com/watch?v=mock_dist01", Source = "YouTube", PositiveVotes = 35, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["Distribución de datos"], Type = "article", Title = "Data Distribution in Distributed Systems", Url = "https://www.mongodb.com/sharding", Source = "MongoDB Docs", PositiveVotes = 18, NegativeVotes = 2, CreatedAt = now },

            new Resource { TopicId = topicIds["Escalamiento horizontal"], Type = "video", Title = "Escalamiento horizontal vs vertical explicado", Url = "https://www.youtube.com/watch?v=mock_scale01", Source = "YouTube", PositiveVotes = 42, NegativeVotes = 4, CreatedAt = now },
            new Resource { TopicId = topicIds["Escalamiento horizontal"], Type = "article", Title = "Horizontal Scaling en bases de datos distribuidas", Url = "https://aws.amazon.com/nosql/horizontal-scaling/", Source = "AWS", PositiveVotes = 25, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["Escalamiento horizontal"], Type = "book", Title = "Designing Data-Intensive Applications - Cap. Escalado", Url = "https://dataintensive.net/", Source = "O'Reilly", PositiveVotes = 0, NegativeVotes = 0, CreatedAt = now },

            // Unidad 4 - Replicación y particionamiento
            new Resource { TopicId = topicIds["Teorema CAP"], Type = "video", Title = "Teorema CAP explicado en 10 minutos - Bases de datos distribuidas", Url = "https://www.youtube.com/watch?v=mock_cap01", Source = "YouTube", PositiveVotes = 55, NegativeVotes = 5, CreatedAt = now },
            new Resource { TopicId = topicIds["Teorema CAP"], Type = "article", Title = "Understanding the CAP Theorem", Url = "https://ocw.mit.edu/courses/cap-theorem", Source = "MIT OCW", PositiveVotes = 32, NegativeVotes = 4, CreatedAt = now },
            new Resource { TopicId = topicIds["Teorema CAP"], Type = "video", Title = "CAP Theorem y su impacto en sistemas NoSQL", Url = "https://www.youtube.com/watch?v=mock_cap02", Source = "YouTube", PositiveVotes = 12, NegativeVotes = 8, CreatedAt = now },

            new Resource { TopicId = topicIds["Modelos de replicación (Master-Slave, Master-Slave Master, peer to peer)"], Type = "video", Title = "Replicación Master-Slave en bases de datos distribuidas", Url = "https://www.youtube.com/watch?v=mock_repl01", Source = "YouTube", PositiveVotes = 28, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["Modelos de replicación (Master-Slave, Master-Slave Master, peer to peer)"], Type = "article", Title = "Database Replication Strategies", Url = "https://www.postgresql.org/docs/current/high-availability.html", Source = "PostgreSQL Docs", PositiveVotes = 15, NegativeVotes = 2, CreatedAt = now },
            new Resource { TopicId = topicIds["Modelos de replicación (Master-Slave, Master-Slave Master, peer to peer)"], Type = "video", Title = "Peer-to-peer replication: Cassandra y Riak", Url = "https://www.youtube.com/watch?v=mock_repl02", Source = "YouTube", PositiveVotes = 0, NegativeVotes = 0, CreatedAt = now },

            new Resource { TopicId = topicIds["Criterios de particionamiento"], Type = "video", Title = "Sharding y particionamiento en NoSQL: estrategias", Url = "https://www.youtube.com/watch?v=mock_part01", Source = "YouTube", PositiveVotes = 36, NegativeVotes = 5, CreatedAt = now },
            new Resource { TopicId = topicIds["Criterios de particionamiento"], Type = "article", Title = "Particionamiento horizontal y vertical en bases distribuidas", Url = "https://www.databricks.com/glossary/data-partitioning", Source = "Databricks", PositiveVotes = 20, NegativeVotes = 4, CreatedAt = now },

            new Resource { TopicId = topicIds["Tipos de consistencia (eventual, por quorum, plena de escritura, plena de lectura)"], Type = "video", Title = "Consistencia eventual vs fuerte en sistemas distribuidos", Url = "https://www.youtube.com/watch?v=mock_cons01", Source = "YouTube", PositiveVotes = 44, NegativeVotes = 4, CreatedAt = now },
            new Resource { TopicId = topicIds["Tipos de consistencia (eventual, por quorum, plena de escritura, plena de lectura)"], Type = "article", Title = "Eventual Consistency - Werner Vogels", Url = "https://www.allthingsdistributed.com/2008/12/eventually_consistent.html", Source = "All Things Distributed", PositiveVotes = 27, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["Tipos de consistencia (eventual, por quorum, plena de escritura, plena de lectura)"], Type = "book", Title = "Designing Data-Intensive Applications - Consistencia y consenso", Url = "https://dataintensive.net/", Source = "O'Reilly", PositiveVotes = 5, NegativeVotes = 2, CreatedAt = now },

            // Unidad 5 - Acceso desde aplicaciones
            new Resource { TopicId = topicIds["Acceso a estructura NoSQL desde una aplicación"], Type = "video", Title = "Conectar Node.js con MongoDB: tutorial completo", Url = "https://www.youtube.com/watch?v=mock_app01", Source = "YouTube", PositiveVotes = 38, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["Acceso a estructura NoSQL desde una aplicación"], Type = "article", Title = "MongoDB Driver para Python: guía de inicio rápido", Url = "https://www.mongodb.com/languages/python/pymongo-tutorial", Source = "MongoDB Docs", PositiveVotes = 22, NegativeVotes = 4, CreatedAt = now },

            new Resource { TopicId = topicIds["Herramientas de conectividad"], Type = "video", Title = "Drivers y ORMs para bases NoSQL en 2024", Url = "https://www.youtube.com/watch?v=mock_conn01", Source = "YouTube", PositiveVotes = 17, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["Herramientas de conectividad"], Type = "article", Title = "Conectividad NoSQL: comparativa de drivers", Url = "https://www.datastax.com/blog/nosql-drivers-comparison", Source = "DataStax", PositiveVotes = 8, NegativeVotes = 4, CreatedAt = now },

            new Resource { TopicId = topicIds["Evaluación de resultados"], Type = "video", Title = "Benchmarking de consultas NoSQL con ejemplos reales", Url = "https://www.youtube.com/watch?v=mock_bench01", Source = "YouTube", PositiveVotes = 14, NegativeVotes = 2, CreatedAt = now },
            new Resource { TopicId = topicIds["Evaluación de resultados"], Type = "article", Title = "NoSQL Performance Benchmarks 2024", Url = "https://www.cockroachlabs.com/blog/nosql-performance-benchmarks/", Source = "CockroachLabs", PositiveVotes = 0, NegativeVotes = 0, CreatedAt = now },

            // Unidad 6 - Grandes volúmenes de datos
            new Resource { TopicId = topicIds["Manejo de grandes volúmenes de datos"], Type = "video", Title = "Big Data: manejo de datos a escala con Hadoop y Spark", Url = "https://www.youtube.com/watch?v=mock_bigdata01", Source = "YouTube", PositiveVotes = 46, NegativeVotes = 4, CreatedAt = now },
            new Resource { TopicId = topicIds["Manejo de grandes volúmenes de datos"], Type = "article", Title = "Introducción a Big Data y arquitecturas de procesamiento", Url = "https://www.ibm.com/topics/big-data", Source = "IBM", PositiveVotes = 30, NegativeVotes = 5, CreatedAt = now },
            new Resource { TopicId = topicIds["Manejo de grandes volúmenes de datos"], Type = "book", Title = "Hadoop: The Definitive Guide - Cap. 1 y 2", Url = "https://www.oreilly.com/library/view/hadoop-the-definitive/9781491901687/", Source = "O'Reilly", PositiveVotes = 18, NegativeVotes = 3, CreatedAt = now },

            new Resource { TopicId = topicIds["Integración de estructuras NoSQL en Data Marts"], Type = "video", Title = "Data Warehouse vs Data Lake vs Data Mart con NoSQL", Url = "https://www.youtube.com/watch?v=mock_datamart01", Source = "YouTube", PositiveVotes = 25, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["Integración de estructuras NoSQL en Data Marts"], Type = "article", Title = "Integrating NoSQL with Data Warehousing", Url = "https://www.databricks.com/glossary/data-mart", Source = "Databricks", PositiveVotes = 12, NegativeVotes = 2, CreatedAt = now },

            new Resource { TopicId = topicIds["Comparación de rendimientos con estructuras relacionales"], Type = "video", Title = "SQL vs NoSQL: benchmarks de rendimiento reales", Url = "https://www.youtube.com/watch?v=mock_perf01", Source = "YouTube", PositiveVotes = 35, NegativeVotes = 5, CreatedAt = now },
            new Resource { TopicId = topicIds["Comparación de rendimientos con estructuras relacionales"], Type = "article", Title = "Performance Comparison: SQL vs NoSQL", Url = "https://www.percona.com/blog/nosql-vs-sql-performance/", Source = "Percona", PositiveVotes = 20, NegativeVotes = 6, CreatedAt = now },
            new Resource { TopicId = topicIds["Comparación de rendimientos con estructuras relacionales"], Type = "video", Title = "Por qué NoSQL puede ser más lento que SQL en ciertos escenarios", Url = "https://www.youtube.com/watch?v=mock_perf02", Source = "YouTube", PositiveVotes = 0, NegativeVotes = 0, CreatedAt = now },

            // ── Seminario de Integración Profesional ──────────────────────
            new Resource { TopicId = topicIds["Definición de problema"], Type = "video", Title = "Cómo definir un problema de forma efectiva - Design Thinking", Url = "https://www.youtube.com/watch?v=mock_prob01", Source = "YouTube", PositiveVotes = 32, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["Definición de problema"], Type = "article", Title = "Problem Framing: técnicas y ejemplos", Url = "https://www.ideo.com/post/problem-framing", Source = "IDEO", PositiveVotes = 18, NegativeVotes = 2, CreatedAt = now },

            new Resource { TopicId = topicIds["Concepto de MVP"], Type = "video", Title = "¿Qué es un MVP? Ejemplos reales de startups exitosas", Url = "https://www.youtube.com/watch?v=mock_mvp01", Source = "YouTube", PositiveVotes = 48, NegativeVotes = 4, CreatedAt = now },
            new Resource { TopicId = topicIds["Concepto de MVP"], Type = "article", Title = "The Lean Startup - MVP explained", Url = "https://theleanstartup.com/principles", Source = "Lean Startup", PositiveVotes = 35, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["Concepto de MVP"], Type = "book", Title = "The Lean Startup - Eric Ries", Url = "https://theleanstartup.com/book", Source = "Crown Business", PositiveVotes = 22, NegativeVotes = 4, CreatedAt = now },

            new Resource { TopicId = topicIds["Metodología de trabajo"], Type = "video", Title = "Metodologías ágiles para proyectos de innovación", Url = "https://www.youtube.com/watch?v=mock_meto01", Source = "YouTube", PositiveVotes = 27, NegativeVotes = 5, CreatedAt = now },
            new Resource { TopicId = topicIds["Metodología de trabajo"], Type = "article", Title = "Metodologías de trabajo para equipos de producto", Url = "https://www.atlassian.com/agile/project-management", Source = "Atlassian", PositiveVotes = 15, NegativeVotes = 3, CreatedAt = now },

            new Resource { TopicId = topicIds["Descubrimiento de desafíos"], Type = "video", Title = "Discovery: cómo identificar desafíos reales de usuarios", Url = "https://www.youtube.com/watch?v=mock_disc01", Source = "YouTube", PositiveVotes = 20, NegativeVotes = 2, CreatedAt = now },
            new Resource { TopicId = topicIds["Descubrimiento de desafíos"], Type = "article", Title = "Continuous Discovery Habits - Teresa Torres", Url = "https://www.producttalk.org/continuous-discovery-habits/", Source = "Product Talk", PositiveVotes = 0, NegativeVotes = 0, CreatedAt = now },

            new Resource { TopicId = topicIds["Narrativa de problemas"], Type = "video", Title = "Cómo construir una narrativa de problema convincente", Url = "https://www.youtube.com/watch?v=mock_narr01", Source = "YouTube", PositiveVotes = 22, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["Narrativa de problemas"], Type = "article", Title = "Storytelling en productos digitales", Url = "https://uxdesign.cc/storytelling-in-product-design", Source = "UX Collective", PositiveVotes = 14, NegativeVotes = 2, CreatedAt = now },

            new Resource { TopicId = topicIds["Métodos de descubrimiento (encuesta, entrevista, observación)"], Type = "video", Title = "Entrevistas de usuario: guía práctica paso a paso", Url = "https://www.youtube.com/watch?v=mock_meth01", Source = "YouTube", PositiveVotes = 40, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["Métodos de descubrimiento (encuesta, entrevista, observación)"], Type = "article", Title = "User Research Methods Overview", Url = "https://www.nngroup.com/articles/which-ux-research-methods/", Source = "NN/g", PositiveVotes = 28, NegativeVotes = 2, CreatedAt = now },
            new Resource { TopicId = topicIds["Métodos de descubrimiento (encuesta, entrevista, observación)"], Type = "book", Title = "Just Enough Research - Erika Hall", Url = "https://abookapart.com/products/just-enough-research", Source = "A Book Apart", PositiveVotes = 16, NegativeVotes = 4, CreatedAt = now },

            new Resource { TopicId = topicIds["Segmentación y target"], Type = "video", Title = "Segmentación de mercado y definición de público objetivo", Url = "https://www.youtube.com/watch?v=mock_seg01", Source = "YouTube", PositiveVotes = 30, NegativeVotes = 4, CreatedAt = now },
            new Resource { TopicId = topicIds["Segmentación y target"], Type = "article", Title = "Market Segmentation: guía completa", Url = "https://www.hubspot.com/marketing/market-segmentation", Source = "HubSpot", PositiveVotes = 18, NegativeVotes = 3, CreatedAt = now },

            new Resource { TopicId = topicIds["Análisis de mercado"], Type = "video", Title = "Cómo hacer un análisis de mercado para tu startup", Url = "https://www.youtube.com/watch?v=mock_market01", Source = "YouTube", PositiveVotes = 26, NegativeVotes = 4, CreatedAt = now },
            new Resource { TopicId = topicIds["Análisis de mercado"], Type = "article", Title = "Market Analysis Framework - Porter's Five Forces", Url = "https://www.strategyzer.com/library/porters-five-forces", Source = "Strategyzer", PositiveVotes = 12, NegativeVotes = 3, CreatedAt = now },

            new Resource { TopicId = topicIds["Design Thinking"], Type = "video", Title = "Design Thinking explicado en 5 pasos con ejemplos", Url = "https://www.youtube.com/watch?v=mock_dt01", Source = "YouTube", PositiveVotes = 52, NegativeVotes = 4, CreatedAt = now },
            new Resource { TopicId = topicIds["Design Thinking"], Type = "article", Title = "Design Thinking: guía completa - IDEO", Url = "https://www.ideo.com/design-thinking", Source = "IDEO", PositiveVotes = 38, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["Design Thinking"], Type = "book", Title = "Change by Design - Tim Brown (IDEO)", Url = "https://www.harpercollins.com/products/change-by-design-tim-brown", Source = "HarperCollins", PositiveVotes = 20, NegativeVotes = 5, CreatedAt = now },

            new Resource { TopicId = topicIds["User Persona"], Type = "video", Title = "Cómo crear User Personas efectivas para tu producto", Url = "https://www.youtube.com/watch?v=mock_persona01", Source = "YouTube", PositiveVotes = 34, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["User Persona"], Type = "article", Title = "Personas: practice and theory - NN/g", Url = "https://www.nngroup.com/articles/persona/", Source = "NN/g", PositiveVotes = 22, NegativeVotes = 2, CreatedAt = now },

            new Resource { TopicId = topicIds["Mapa de empatía"], Type = "video", Title = "Mapa de empatía: qué es y cómo construirlo", Url = "https://www.youtube.com/watch?v=mock_empathy01", Source = "YouTube", PositiveVotes = 28, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["Mapa de empatía"], Type = "article", Title = "Empathy Mapping: A Guide to Getting Inside a User's Head", Url = "https://www.nngroup.com/articles/empathy-mapping/", Source = "NN/g", PositiveVotes = 16, NegativeVotes = 2, CreatedAt = now },

            new Resource { TopicId = topicIds["Escenario actual"], Type = "video", Title = "Current State vs Future State Mapping en UX", Url = "https://www.youtube.com/watch?v=mock_current01", Source = "YouTube", PositiveVotes = 15, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["Escenario actual"], Type = "article", Title = "As-Is Scenario Mapping para entender el estado actual", Url = "https://www.interaction-design.org/literature/article/as-is-scenario-mapping", Source = "IxDF", PositiveVotes = 0, NegativeVotes = 0, CreatedAt = now },

            new Resource { TopicId = topicIds["User Journey Map"], Type = "video", Title = "User Journey Map completo con ejemplo real", Url = "https://www.youtube.com/watch?v=mock_ujm01", Source = "YouTube", PositiveVotes = 45, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["User Journey Map"], Type = "article", Title = "Journey Mapping 101 - NN/g", Url = "https://www.nngroup.com/articles/journey-mapping-101/", Source = "NN/g", PositiveVotes = 30, NegativeVotes = 2, CreatedAt = now },
            new Resource { TopicId = topicIds["User Journey Map"], Type = "book", Title = "Mapping Experiences - James Kalbach", Url = "https://www.oreilly.com/library/view/mapping-experiences/9781491923528/", Source = "O'Reilly", PositiveVotes = 18, NegativeVotes = 4, CreatedAt = now },

            new Resource { TopicId = topicIds["Focus Groups"], Type = "video", Title = "Focus Groups: técnica y análisis de resultados", Url = "https://www.youtube.com/watch?v=mock_focus01", Source = "YouTube", PositiveVotes = 20, NegativeVotes = 4, CreatedAt = now },
            new Resource { TopicId = topicIds["Focus Groups"], Type = "article", Title = "When to Use Focus Groups - NN/g", Url = "https://www.nngroup.com/articles/focus-groups/", Source = "NN/g", PositiveVotes = 12, NegativeVotes = 3, CreatedAt = now },

            new Resource { TopicId = topicIds["Ideación de soluciones y brainstorming"], Type = "video", Title = "Técnicas de brainstorming para equipos de innovación", Url = "https://www.youtube.com/watch?v=mock_brain01", Source = "YouTube", PositiveVotes = 38, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["Ideación de soluciones y brainstorming"], Type = "article", Title = "Brainstorming con constraints: técnicas avanzadas", Url = "https://www.ideou.com/blogs/inspiration/brainstorming-techniques", Source = "IDEO U", PositiveVotes = 22, NegativeVotes = 4, CreatedAt = now },

            new Resource { TopicId = topicIds["Roadmap"], Type = "video", Title = "Cómo construir un Product Roadmap orientado a outcomes", Url = "https://www.youtube.com/watch?v=mock_road01", Source = "YouTube", PositiveVotes = 35, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["Roadmap"], Type = "article", Title = "Product Roadmap Guide - Atlassian", Url = "https://www.atlassian.com/agile/product-management/roadmaps", Source = "Atlassian", PositiveVotes = 24, NegativeVotes = 4, CreatedAt = now },

            new Resource { TopicId = topicIds["Estrategia de negocios digitales"], Type = "video", Title = "Estrategia digital: cómo construir un modelo de negocio online", Url = "https://www.youtube.com/watch?v=mock_strat01", Source = "YouTube", PositiveVotes = 28, NegativeVotes = 5, CreatedAt = now },
            new Resource { TopicId = topicIds["Estrategia de negocios digitales"], Type = "book", Title = "Business Model Generation - Osterwalder y Pigneur", Url = "https://www.strategyzer.com/books/business-model-generation", Source = "Strategyzer", PositiveVotes = 40, NegativeVotes = 3, CreatedAt = now },

            new Resource { TopicId = topicIds["Narrativa de solución"], Type = "video", Title = "Cómo presentar tu solución con narrativa convincente", Url = "https://www.youtube.com/watch?v=mock_sol01", Source = "YouTube", PositiveVotes = 22, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["Narrativa de solución"], Type = "article", Title = "Solution Storytelling para productos digitales", Url = "https://uxdesign.cc/solution-storytelling", Source = "UX Collective", PositiveVotes = 0, NegativeVotes = 0, CreatedAt = now },

            new Resource { TopicId = topicIds["Historias de usuario y criterios de aceptación"], Type = "video", Title = "User Stories y criterios de aceptación: guía práctica", Url = "https://www.youtube.com/watch?v=mock_us01", Source = "YouTube", PositiveVotes = 44, NegativeVotes = 4, CreatedAt = now },
            new Resource { TopicId = topicIds["Historias de usuario y criterios de aceptación"], Type = "article", Title = "User Stories - Atlassian Agile Coach", Url = "https://www.atlassian.com/agile/project-management/user-stories", Source = "Atlassian", PositiveVotes = 32, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["Historias de usuario y criterios de aceptación"], Type = "book", Title = "User Story Mapping - Jeff Patton", Url = "https://www.jpattonassociates.com/user-story-mapping/", Source = "O'Reilly", PositiveVotes = 20, NegativeVotes = 2, CreatedAt = now },

            new Resource { TopicId = topicIds["SPIDR"], Type = "video", Title = "SPIDR: cómo dividir user stories grandes", Url = "https://www.youtube.com/watch?v=mock_spidr01", Source = "YouTube", PositiveVotes = 18, NegativeVotes = 2, CreatedAt = now },
            new Resource { TopicId = topicIds["SPIDR"], Type = "article", Title = "Splitting User Stories with SPIDR", Url = "https://www.mountaingoatsoftware.com/blog/splitting-user-stories", Source = "Mountain Goat", PositiveVotes = 12, NegativeVotes = 3, CreatedAt = now },

            new Resource { TopicId = topicIds["User Story Map"], Type = "video", Title = "User Story Mapping con Jeff Patton - Resumen", Url = "https://www.youtube.com/watch?v=mock_usm01", Source = "YouTube", PositiveVotes = 30, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["User Story Map"], Type = "article", Title = "Story Map tutorial - Atlassian", Url = "https://www.atlassian.com/agile/project-management/user-story-mapping", Source = "Atlassian", PositiveVotes = 0, NegativeVotes = 0, CreatedAt = now },

            new Resource { TopicId = topicIds["User flow y user task"], Type = "video", Title = "User Flow y Task Flow: diferencias y cómo crearlos", Url = "https://www.youtube.com/watch?v=mock_uf01", Source = "YouTube", PositiveVotes = 25, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["User flow y user task"], Type = "article", Title = "User Flows vs Task Flows - NN/g", Url = "https://www.nngroup.com/articles/user-flow-diagrams/", Source = "NN/g", PositiveVotes = 16, NegativeVotes = 2, CreatedAt = now },

            new Resource { TopicId = topicIds["Taller de UX/UI"], Type = "video", Title = "Principios de UX/UI para no diseñadores", Url = "https://www.youtube.com/watch?v=mock_uxui01", Source = "YouTube", PositiveVotes = 42, NegativeVotes = 4, CreatedAt = now },
            new Resource { TopicId = topicIds["Taller de UX/UI"], Type = "article", Title = "10 Usability Heuristics - Nielsen Norman Group", Url = "https://www.nngroup.com/articles/ten-usability-heuristics/", Source = "NN/g", PositiveVotes = 35, NegativeVotes = 2, CreatedAt = now },

            new Resource { TopicId = topicIds["Sprints y metodología Scrum"], Type = "video", Title = "Scrum en 10 minutos: sprints, roles y ceremonias", Url = "https://www.youtube.com/watch?v=mock_scrum01", Source = "YouTube", PositiveVotes = 55, NegativeVotes = 4, CreatedAt = now },
            new Resource { TopicId = topicIds["Sprints y metodología Scrum"], Type = "article", Title = "Scrum Guide 2020 - Oficial", Url = "https://www.scrumguides.org/scrum-guide.html", Source = "Scrum.org", PositiveVotes = 40, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["Sprints y metodología Scrum"], Type = "book", Title = "Scrum: The Art of Doing Twice the Work in Half the Time", Url = "https://www.scruminc.com/new-scrum-the-book/", Source = "Crown Business", PositiveVotes = 28, NegativeVotes = 5, CreatedAt = now },

            new Resource { TopicId = topicIds["Reviews y retrospectivas"], Type = "video", Title = "Sprint Review y Sprint Retrospective: cómo facilitarlos", Url = "https://www.youtube.com/watch?v=mock_retro01", Source = "YouTube", PositiveVotes = 32, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["Reviews y retrospectivas"], Type = "article", Title = "Retrospectives for Agile Teams - Atlassian", Url = "https://www.atlassian.com/agile/scrum/retrospectives", Source = "Atlassian", PositiveVotes = 18, NegativeVotes = 2, CreatedAt = now },

            new Resource { TopicId = topicIds["Feedback 360"], Type = "video", Title = "Feedback 360°: cómo implementarlo en tu equipo", Url = "https://www.youtube.com/watch?v=mock_feed01", Source = "YouTube", PositiveVotes = 20, NegativeVotes = 4, CreatedAt = now },
            new Resource { TopicId = topicIds["Feedback 360"], Type = "article", Title = "360 Feedback: guía completa de implementación", Url = "https://www.shrm.org/topics-tools/tools/how-to-guides/how-to-establish-360-degree-feedback-process", Source = "SHRM", PositiveVotes = 0, NegativeVotes = 0, CreatedAt = now },

            new Resource { TopicId = topicIds["Pitch y oratoria"], Type = "video", Title = "Cómo hacer un pitch ganador en 5 minutos", Url = "https://www.youtube.com/watch?v=mock_pitch01", Source = "YouTube", PositiveVotes = 48, NegativeVotes = 5, CreatedAt = now },
            new Resource { TopicId = topicIds["Pitch y oratoria"], Type = "article", Title = "The Perfect Pitch Deck - Y Combinator", Url = "https://www.ycombinator.com/library/2u-how-to-design-a-better-pitch-deck", Source = "Y Combinator", PositiveVotes = 35, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["Pitch y oratoria"], Type = "video", Title = "Oratoria y comunicación efectiva para ingenieros", Url = "https://www.youtube.com/watch?v=mock_pitch02", Source = "YouTube", PositiveVotes = 22, NegativeVotes = 5, CreatedAt = now },

            new Resource { TopicId = topicIds["Negociación"], Type = "video", Title = "Técnicas de negociación basadas en Harvard Negotiation Project", Url = "https://www.youtube.com/watch?v=mock_neg01", Source = "YouTube", PositiveVotes = 36, NegativeVotes = 4, CreatedAt = now },
            new Resource { TopicId = topicIds["Negociación"], Type = "book", Title = "Getting to Yes - Fisher y Ury", Url = "https://www.penguin.com/books/getting-to-yes/", Source = "Penguin Books", PositiveVotes = 42, NegativeVotes = 3, CreatedAt = now },

            // ── Estadística Aplicada ──────────────────────────────────────
            new Resource { TopicId = topicIds["Medidas de tendencia central (media, mediana, moda)"], Type = "video", Title = "Media, mediana y moda explicadas con ejemplos", Url = "https://www.youtube.com/watch?v=mock_stat01", Source = "YouTube", PositiveVotes = 45, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["Medidas de tendencia central (media, mediana, moda)"], Type = "article", Title = "Measures of Central Tendency - Khan Academy", Url = "https://www.khanacademy.org/math/statistics-probability/summarizing-quantitative-data", Source = "Khan Academy", PositiveVotes = 38, NegativeVotes = 2, CreatedAt = now },

            new Resource { TopicId = topicIds["Medidas de dispersión (varianza, desvío estándar)"], Type = "video", Title = "Varianza y desvío estándar: intuición y cálculo", Url = "https://www.youtube.com/watch?v=mock_stat02", Source = "YouTube", PositiveVotes = 40, NegativeVotes = 4, CreatedAt = now },
            new Resource { TopicId = topicIds["Medidas de dispersión (varianza, desvío estándar)"], Type = "article", Title = "Variance and Standard Deviation - Khan Academy", Url = "https://www.khanacademy.org/math/statistics-probability/describing-relationships-quantitative-data", Source = "Khan Academy", PositiveVotes = 28, NegativeVotes = 3, CreatedAt = now },

            new Resource { TopicId = topicIds["Visualización de datos: histogramas y boxplots"], Type = "video", Title = "Histogramas y boxplots: cómo leerlos e interpretarlos", Url = "https://www.youtube.com/watch?v=mock_stat03", Source = "YouTube", PositiveVotes = 32, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["Visualización de datos: histogramas y boxplots"], Type = "article", Title = "Data Visualization with Python - histogramas y boxplots", Url = "https://matplotlib.org/stable/gallery/statistics/index.html", Source = "Matplotlib Docs", PositiveVotes = 20, NegativeVotes = 4, CreatedAt = now },

            new Resource { TopicId = topicIds["Conceptos básicos de probabilidad"], Type = "video", Title = "Probabilidad básica desde cero con ejercicios", Url = "https://www.youtube.com/watch?v=mock_prob02", Source = "YouTube", PositiveVotes = 42, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["Conceptos básicos de probabilidad"], Type = "article", Title = "Basic Probability - Khan Academy", Url = "https://www.khanacademy.org/math/statistics-probability/probability-library", Source = "Khan Academy", PositiveVotes = 35, NegativeVotes = 2, CreatedAt = now },

            new Resource { TopicId = topicIds["Probabilidad condicional e independencia"], Type = "video", Title = "Probabilidad condicional e independencia con ejemplos", Url = "https://www.youtube.com/watch?v=mock_cond01", Source = "YouTube", PositiveVotes = 38, NegativeVotes = 4, CreatedAt = now },
            new Resource { TopicId = topicIds["Probabilidad condicional e independencia"], Type = "article", Title = "Conditional Probability - MIT OCW", Url = "https://ocw.mit.edu/courses/18-05-introduction-to-probability-and-statistics-spring-2022/", Source = "MIT OCW", PositiveVotes = 25, NegativeVotes = 3, CreatedAt = now },

            new Resource { TopicId = topicIds["Teorema de Bayes"], Type = "video", Title = "Teorema de Bayes explicado con ejemplos visuales", Url = "https://www.youtube.com/watch?v=mock_bayes01", Source = "YouTube", PositiveVotes = 50, NegativeVotes = 4, CreatedAt = now },
            new Resource { TopicId = topicIds["Teorema de Bayes"], Type = "article", Title = "Bayes' Theorem - 3Blue1Brown article", Url = "https://www.3blue1brown.com/lessons/bayes-theorem", Source = "3Blue1Brown", PositiveVotes = 45, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["Teorema de Bayes"], Type = "video", Title = "Bayes theorem: the geometry of changing beliefs - 3Blue1Brown", Url = "https://www.youtube.com/watch?v=mock_bayes02", Source = "YouTube", PositiveVotes = 60, NegativeVotes = 2, CreatedAt = now },

            new Resource { TopicId = topicIds["Variables aleatorias discretas y continuas"], Type = "video", Title = "Variables aleatorias: discretas vs continuas con ejemplos", Url = "https://www.youtube.com/watch?v=mock_var01", Source = "YouTube", PositiveVotes = 33, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["Variables aleatorias discretas y continuas"], Type = "article", Title = "Random Variables - Khan Academy", Url = "https://www.khanacademy.org/math/statistics-probability/random-variables-stats-library", Source = "Khan Academy", PositiveVotes = 0, NegativeVotes = 0, CreatedAt = now },

            new Resource { TopicId = topicIds["Distribución binomial y de Poisson"], Type = "video", Title = "Distribución binomial y Poisson: cuándo usarlas", Url = "https://www.youtube.com/watch?v=mock_dist01b", Source = "YouTube", PositiveVotes = 36, NegativeVotes = 4, CreatedAt = now },
            new Resource { TopicId = topicIds["Distribución binomial y de Poisson"], Type = "article", Title = "Binomial and Poisson Distributions - MIT OCW", Url = "https://ocw.mit.edu/courses/distributions", Source = "MIT OCW", PositiveVotes = 22, NegativeVotes = 3, CreatedAt = now },

            new Resource { TopicId = topicIds["Distribución normal y teorema central del límite"], Type = "video", Title = "Distribución normal y TCL explicados visualmente", Url = "https://www.youtube.com/watch?v=mock_normal01", Source = "YouTube", PositiveVotes = 48, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["Distribución normal y teorema central del límite"], Type = "article", Title = "Central Limit Theorem - Khan Academy", Url = "https://www.khanacademy.org/math/statistics-probability/sampling-distributions-library/sample-means/a/central-limit-theorem-review", Source = "Khan Academy", PositiveVotes = 40, NegativeVotes = 2, CreatedAt = now },
            new Resource { TopicId = topicIds["Distribución normal y teorema central del límite"], Type = "video", Title = "La distribución normal y su importancia en estadística", Url = "https://www.youtube.com/watch?v=mock_normal02", Source = "YouTube", PositiveVotes = 0, NegativeVotes = 0, CreatedAt = now },

            new Resource { TopicId = topicIds["Distribuciones t, chi-cuadrado y F"], Type = "video", Title = "Distribuciones t de Student, chi-cuadrado y F: cuándo usarlas", Url = "https://www.youtube.com/watch?v=mock_tdist01", Source = "YouTube", PositiveVotes = 30, NegativeVotes = 5, CreatedAt = now },
            new Resource { TopicId = topicIds["Distribuciones t, chi-cuadrado y F"], Type = "article", Title = "T, Chi-square and F Distributions overview - Stats textbook", Url = "https://ocw.mit.edu/courses/t-chi-f-distributions", Source = "MIT OCW", PositiveVotes = 18, NegativeVotes = 4, CreatedAt = now },

            new Resource { TopicId = topicIds["Estimación puntual e intervalos de confianza"], Type = "video", Title = "Intervalos de confianza explicados con ejemplos prácticos", Url = "https://www.youtube.com/watch?v=mock_ci01", Source = "YouTube", PositiveVotes = 42, NegativeVotes = 4, CreatedAt = now },
            new Resource { TopicId = topicIds["Estimación puntual e intervalos de confianza"], Type = "article", Title = "Confidence Intervals - Khan Academy", Url = "https://www.khanacademy.org/math/statistics-probability/confidence-intervals-one-sample", Source = "Khan Academy", PositiveVotes = 35, NegativeVotes = 3, CreatedAt = now },

            new Resource { TopicId = topicIds["Pruebas de hipótesis: errores tipo I y tipo II"], Type = "video", Title = "Pruebas de hipótesis: errores tipo I y tipo II con ejemplos", Url = "https://www.youtube.com/watch?v=mock_hyp01", Source = "YouTube", PositiveVotes = 38, NegativeVotes = 5, CreatedAt = now },
            new Resource { TopicId = topicIds["Pruebas de hipótesis: errores tipo I y tipo II"], Type = "article", Title = "Hypothesis Testing: Type I and Type II errors", Url = "https://www.statisticshowto.com/probability-and-statistics/hypothesis-testing/type-i-error/", Source = "Statistics How To", PositiveVotes = 22, NegativeVotes = 4, CreatedAt = now },
            new Resource { TopicId = topicIds["Pruebas de hipótesis: errores tipo I y tipo II"], Type = "book", Title = "Introduction to Statistical Learning - Cap. 1", Url = "https://www.statlearning.com/", Source = "Stanford", PositiveVotes = 28, NegativeVotes = 3, CreatedAt = now },

            new Resource { TopicId = topicIds["Prueba t de Student y prueba chi-cuadrado"], Type = "video", Title = "T-test y chi-cuadrado: cuándo y cómo aplicarlos", Url = "https://www.youtube.com/watch?v=mock_ttest01", Source = "YouTube", PositiveVotes = 35, NegativeVotes = 4, CreatedAt = now },
            new Resource { TopicId = topicIds["Prueba t de Student y prueba chi-cuadrado"], Type = "article", Title = "T-test and Chi-square test guide", Url = "https://www.graphpad.com/guides/statistics", Source = "GraphPad", PositiveVotes = 20, NegativeVotes = 3, CreatedAt = now },

            new Resource { TopicId = topicIds["p-valor e interpretación de resultados"], Type = "video", Title = "El p-valor explicado de una vez por todas", Url = "https://www.youtube.com/watch?v=mock_pval01", Source = "YouTube", PositiveVotes = 55, NegativeVotes = 5, CreatedAt = now },
            new Resource { TopicId = topicIds["p-valor e interpretación de resultados"], Type = "article", Title = "What is a p-value? - StatQuest", Url = "https://www.youtube.com/watch?v=mock_pval02", Source = "StatQuest", PositiveVotes = 48, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["p-valor e interpretación de resultados"], Type = "article", Title = "Misinterpretations of p-values - Nature", Url = "https://www.nature.com/articles/506150a", Source = "Nature", PositiveVotes = 0, NegativeVotes = 0, CreatedAt = now },

            new Resource { TopicId = topicIds["Correlación de Pearson y Spearman"], Type = "video", Title = "Correlación de Pearson y Spearman: diferencias clave", Url = "https://www.youtube.com/watch?v=mock_corr01", Source = "YouTube", PositiveVotes = 40, NegativeVotes = 4, CreatedAt = now },
            new Resource { TopicId = topicIds["Correlación de Pearson y Spearman"], Type = "article", Title = "Pearson vs Spearman Correlation - Statistics How To", Url = "https://www.statisticshowto.com/pearson-vs-spearman/", Source = "Statistics How To", PositiveVotes = 25, NegativeVotes = 3, CreatedAt = now },

            new Resource { TopicId = topicIds["Regresión lineal simple"], Type = "video", Title = "Regresión lineal simple: intuición y cálculo paso a paso", Url = "https://www.youtube.com/watch?v=mock_reg01", Source = "YouTube", PositiveVotes = 48, NegativeVotes = 4, CreatedAt = now },
            new Resource { TopicId = topicIds["Regresión lineal simple"], Type = "article", Title = "Simple Linear Regression - Khan Academy", Url = "https://www.khanacademy.org/math/statistics-probability/describing-relationships-quantitative-data/regression-library/a/introduction-to-residuals", Source = "Khan Academy", PositiveVotes = 38, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["Regresión lineal simple"], Type = "book", Title = "An Introduction to Statistical Learning - Regresión cap. 3", Url = "https://www.statlearning.com/", Source = "Stanford", PositiveVotes = 30, NegativeVotes = 2, CreatedAt = now },

            new Resource { TopicId = topicIds["Regresión lineal múltiple"], Type = "video", Title = "Regresión lineal múltiple con Python y scikit-learn", Url = "https://www.youtube.com/watch?v=mock_reg02", Source = "YouTube", PositiveVotes = 42, NegativeVotes = 5, CreatedAt = now },
            new Resource { TopicId = topicIds["Regresión lineal múltiple"], Type = "article", Title = "Multiple Linear Regression - Statistics How To", Url = "https://www.statisticshowto.com/probability-and-statistics/regression-analysis/multiple-regression/", Source = "Statistics How To", PositiveVotes = 0, NegativeVotes = 0, CreatedAt = now },

            new Resource { TopicId = topicIds["Componentes de una serie temporal (tendencia, estacionalidad, ruido)"], Type = "video", Title = "Series temporales: tendencia, estacionalidad y ruido explicados", Url = "https://www.youtube.com/watch?v=mock_ts01", Source = "YouTube", PositiveVotes = 36, NegativeVotes = 3, CreatedAt = now },
            new Resource { TopicId = topicIds["Componentes de una serie temporal (tendencia, estacionalidad, ruido)"], Type = "article", Title = "Time Series Components - Forecasting: Principles and Practice", Url = "https://otexts.com/fpp3/components.html", Source = "OTexts", PositiveVotes = 28, NegativeVotes = 2, CreatedAt = now },

            new Resource { TopicId = topicIds["Modelos de suavizado exponencial"], Type = "video", Title = "Suavizado exponencial simple y Holt-Winters explicados", Url = "https://www.youtube.com/watch?v=mock_exp01", Source = "YouTube", PositiveVotes = 25, NegativeVotes = 4, CreatedAt = now },
            new Resource { TopicId = topicIds["Modelos de suavizado exponencial"], Type = "article", Title = "Exponential Smoothing - Forecasting: Principles and Practice", Url = "https://otexts.com/fpp3/expsmooth.html", Source = "OTexts", PositiveVotes = 18, NegativeVotes = 3, CreatedAt = now },

            new Resource { TopicId = topicIds["Introducción a modelos ARIMA"], Type = "video", Title = "ARIMA explicado desde cero con ejemplos en Python", Url = "https://www.youtube.com/watch?v=mock_arima01", Source = "YouTube", PositiveVotes = 44, NegativeVotes = 5, CreatedAt = now },
            new Resource { TopicId = topicIds["Introducción a modelos ARIMA"], Type = "article", Title = "ARIMA Models - Forecasting: Principles and Practice", Url = "https://otexts.com/fpp3/arima.html", Source = "OTexts", PositiveVotes = 32, NegativeVotes = 4, CreatedAt = now },
            new Resource { TopicId = topicIds["Introducción a modelos ARIMA"], Type = "book", Title = "Forecasting: Principles and Practice (FPP3) - Hyndman & Athanasopoulos", Url = "https://otexts.com/fpp3/", Source = "OTexts", PositiveVotes = 38, NegativeVotes = 3, CreatedAt = now },
        };

        db.Resources.AddRange(resources);
        await db.SaveChangesAsync();

        // ── FEEDBACKS MOCKEADOS ────────────────────────────────────────────
        List<Resource> savedResources = db.Resources.ToList();

        // Tomamos los primeros 30 recursos y generamos feedbacks coherentes con los votos existentes
        List<Feedback> feedbacks = new();
        string[] userIds = { "user001", "user002", "user003", "user004", "user005", "user006", "user007", "user008", "user009", "user010" };

        for (int i = 0; i < Math.Min(30, savedResources.Count); i++)
        {
            Resource res = savedResources[i];
            if (res.PositiveVotes == 0 && res.NegativeVotes == 0) continue;

            int totalVotes = res.PositiveVotes + res.NegativeVotes;
            int feedbacksToCreate = Math.Min(totalVotes, userIds.Length);

            for (int j = 0; j < feedbacksToCreate; j++)
            {
                bool isHelpful = j < (int)Math.Round((double)res.PositiveVotes / totalVotes * feedbacksToCreate);
                feedbacks.Add(new Feedback
                {
                    ResourceId = res.Id,
                    UserId = userIds[j],
                    IsHelpful = isHelpful,
                    CreatedAt = now.AddMinutes(-j * 15)
                });
            }
        }

        db.Feedbacks.AddRange(feedbacks);
        await db.SaveChangesAsync();
    }

    public static Dictionary<string, List<(int unitNumber, string unitName, string topicName, int orderIndex)>> GetMockTopics()
    {
        return new Dictionary<string, List<(int, string, string, int)>>(StringComparer.OrdinalIgnoreCase)
        {
            ["Ingeniería de Datos II"] = new()
            {
                (1, "Introducción a NoSQL", "Concepto de NoSQL", 1),
                (1, "Introducción a NoSQL", "Modelo Relacional vs estructuras No Relacionales", 2),
                (1, "Introducción a NoSQL", "Criterio de selección entre ambos modelos", 3),
                (1, "Introducción a NoSQL", "Relación con volúmenes de datos y de consultas", 4),
                (1, "Introducción a NoSQL", "ACID en NoSQL", 5),
                (2, "Modelos NoSQL", "Distintos modelos NoSQL (clave-valor, documentos, grafos, columnares)", 1),
                (2, "Modelos NoSQL", "Implementación y funcionamiento de cada modelo", 2),
                (2, "Modelos NoSQL", "Comparación de las distintas soluciones", 3),
                (3, "Administración de datos no estructurados", "Administración y recuperación desde fuentes de datos no estructurados", 1),
                (3, "Administración de datos no estructurados", "Interfaces de administración", 2),
                (3, "Administración de datos no estructurados", "Técnicas de acceso", 3),
                (3, "Administración de datos no estructurados", "Distribución de datos", 4),
                (3, "Administración de datos no estructurados", "Escalamiento horizontal", 5),
                (4, "Replicación y particionamiento", "Teorema CAP", 1),
                (4, "Replicación y particionamiento", "Modelos de replicación (Master-Slave, Master-Slave Master, peer to peer)", 2),
                (4, "Replicación y particionamiento", "Criterios de particionamiento", 3),
                (4, "Replicación y particionamiento", "Tipos de consistencia (eventual, por quorum, plena de escritura, plena de lectura)", 4),
                (5, "Acceso desde aplicaciones", "Acceso a estructura NoSQL desde una aplicación", 1),
                (5, "Acceso desde aplicaciones", "Herramientas de conectividad", 2),
                (5, "Acceso desde aplicaciones", "Evaluación de resultados", 3),
                (6, "Grandes volúmenes de datos", "Manejo de grandes volúmenes de datos", 1),
                (6, "Grandes volúmenes de datos", "Integración de estructuras NoSQL en Data Marts", 2),
                (6, "Grandes volúmenes de datos", "Comparación de rendimientos con estructuras relacionales", 3),
            },
            ["Seminario de Integración Profesional"] = new()
            {
                (1, "Definición de problema y descubrimiento", "Definición de problema", 1),
                (1, "Definición de problema y descubrimiento", "Concepto de MVP", 2),
                (1, "Definición de problema y descubrimiento", "Metodología de trabajo", 3),
                (1, "Definición de problema y descubrimiento", "Descubrimiento de desafíos", 4),
                (2, "Investigación y análisis", "Narrativa de problemas", 1),
                (2, "Investigación y análisis", "Métodos de descubrimiento (encuesta, entrevista, observación)", 2),
                (2, "Investigación y análisis", "Segmentación y target", 3),
                (2, "Investigación y análisis", "Análisis de mercado", 4),
                (3, "Design Thinking y experiencia de usuario", "Design Thinking", 1),
                (3, "Design Thinking y experiencia de usuario", "User Persona", 2),
                (3, "Design Thinking y experiencia de usuario", "Mapa de empatía", 3),
                (3, "Design Thinking y experiencia de usuario", "Escenario actual", 4),
                (3, "Design Thinking y experiencia de usuario", "User Journey Map", 5),
                (4, "Ideación y estrategia", "Focus Groups", 1),
                (4, "Ideación y estrategia", "Ideación de soluciones y brainstorming", 2),
                (4, "Ideación y estrategia", "Roadmap", 3),
                (4, "Ideación y estrategia", "Estrategia de negocios digitales", 4),
                (4, "Ideación y estrategia", "Narrativa de solución", 5),
                (5, "Historias de usuario y diseño", "Historias de usuario y criterios de aceptación", 1),
                (5, "Historias de usuario y diseño", "SPIDR", 2),
                (5, "Historias de usuario y diseño", "User Story Map", 3),
                (5, "Historias de usuario y diseño", "User flow y user task", 4),
                (5, "Historias de usuario y diseño", "Taller de UX/UI", 5),
                (6, "Gestión ágil", "Sprints y metodología Scrum", 1),
                (6, "Gestión ágil", "Reviews y retrospectivas", 2),
                (6, "Gestión ágil", "Feedback 360", 3),
                (6, "Gestión ágil", "Pitch y oratoria", 4),
                (6, "Gestión ágil", "Negociación", 5),
            },
            ["Estadística Aplicada"] = new()
            {
                (1, "Estadística descriptiva", "Medidas de tendencia central (media, mediana, moda)", 1),
                (1, "Estadística descriptiva", "Medidas de dispersión (varianza, desvío estándar)", 2),
                (1, "Estadística descriptiva", "Visualización de datos: histogramas y boxplots", 3),
                (2, "Probabilidad", "Conceptos básicos de probabilidad", 1),
                (2, "Probabilidad", "Probabilidad condicional e independencia", 2),
                (2, "Probabilidad", "Teorema de Bayes", 3),
                (2, "Probabilidad", "Variables aleatorias discretas y continuas", 4),
                (3, "Distribuciones de probabilidad", "Distribución binomial y de Poisson", 1),
                (3, "Distribuciones de probabilidad", "Distribución normal y teorema central del límite", 2),
                (3, "Distribuciones de probabilidad", "Distribuciones t, chi-cuadrado y F", 3),
                (4, "Inferencia estadística", "Estimación puntual e intervalos de confianza", 1),
                (4, "Inferencia estadística", "Pruebas de hipótesis: errores tipo I y tipo II", 2),
                (4, "Inferencia estadística", "Prueba t de Student y prueba chi-cuadrado", 3),
                (4, "Inferencia estadística", "p-valor e interpretación de resultados", 4),
                (5, "Regresión y correlación", "Correlación de Pearson y Spearman", 1),
                (5, "Regresión y correlación", "Regresión lineal simple", 2),
                (5, "Regresión y correlación", "Regresión lineal múltiple", 3),
                (6, "Series temporales", "Componentes de una serie temporal (tendencia, estacionalidad, ruido)", 1),
                (6, "Series temporales", "Modelos de suavizado exponencial", 2),
                (6, "Series temporales", "Introducción a modelos ARIMA", 3),
            },
        };
    }
}
