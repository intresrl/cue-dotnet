// Minimal example of a match expression over cases with a discriminator field

// Define base schema with common discriminator field
#BaseConfig: {
	type: string
	name: string
}

// Different configuration types with same discriminator "type"
#DatabaseConfig: #BaseConfig & {
	type:     "database"
	host:     string
	port:     int
	username: string
}

#CacheConfig: #BaseConfig & {
	type: "cache"
	ttl:  int
	maxSize: int
}

#LogConfig: #BaseConfig & {
	type:  "log"
	level: string
	output: string
}

// Example configs
dbConfig: #DatabaseConfig & {
	type:     "database"
	name:     "mydb"
	host:     "localhost"
	port:     5432
	username: "admin"
}

cacheConfig: #CacheConfig & {
	type:    "cache"
	name:    "redis"
	ttl:     3600
	maxSize: 1000
}

logConfig: #LogConfig & {
	type:   "log"
	name:   "app_logs"
	level:  "info"
	output: "stdout"
}

// Match expression over cases with discriminator field
configs: [dbConfig, cacheConfig, logConfig]

configDescription: {
	for i, config in configs {
		"\(config.name)": {
			match config.type {
				case "database":
					"\(config.name) - Database on \(config.host):\(config.port)"
				case "cache":
					"\(config.name) - Cache with TTL \(config.ttl)s, max \(config.maxSize) items"
				case "log":
					"\(config.name) - Logger at \(config.level) level, output to \(config.output)"
			}
		}
	}
}
