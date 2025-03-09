#dev #react #go #mysql #docker  #docker-compose #nginx #logging #oauth #discord #t-rpg #trpg #ai

---
Complete remaster of game [Famyok](https://www.famyok.cz)
It will be separated into:
- Static [[#Promotional website]]
- React app ([[#The Game]])
- [[#Admin Panel]]
- [[#PhpMyAdmin]] for DB management
- [[#Monitoring Panel]]
- [[#Web Developer Environment]]

The current data from the current MySQL db will be migrated to the new db.
The design of websites and app has to be reworked. New designs have to be proposed

To discuss:
- [ ] Propose to the community to come with re-designs
- [ ] Ask if the came up infrastructure solution sounds good

# Famyok Kolosus

## Interface

### Promotional website

Similar to this layout, but a little bit more modern
![[Pasted image 20250224055606.png]]

### The Game

Similar to the [[#Promotional website]], but it will be distinct enough, so it'd be immersive for the player
![[Pasted image 20250224055606.png]]

### Admin Panel

New look, Something boring and simple. Just simple operations. To approve, modify, delete, create, view data

### PhpMyAdmin

Similar to the already deployed MySQL management on forpsi, but newer
![[2-29.png]]

### Monitoring Panel

Displaying current errors, bugs and events. What has changed, who has logged in and bunch more

Once platform will be decided, there needs to be **someone who will make dashboards and monitoring  views**

| Monitoring Tool    | Can Run Locally    | Resource Usage   | How to Run Locally      |
| ------------------ | ------------------ | ---------------- | ----------------------- |
| **Loki**           | Yes                | Low to Moderate  | Docker Compose          |
| **Elastic**        | Yes                | Moderate to High | Docker Compose          |
| **Graylog**        | Yes                | Moderate         | Docker Compose          |
| **Splunk**         | Yes (Free Version) | High             | Docker                  |


Logging server will be provided by Fluentd probably
**There multiple options:**

#### Loki

![[grafana-2.png]]

- **Resource Usage**: Loki is lighter on resources compared to Elasticsearch, making it suitable for local setups. Grafana requires a bit more resources but is generally lightweight.
- **Loki** is a log aggregation system optimized for Kubernetes, and **Grafana** is the visualization tool that allows you to query logs from Loki and visualize them alongside metrics.
- **Fluentd Integration**: Fluentd can easily send logs to Loki using the **Loki** output plugin, which works well with Kubernetes and Docker-based environments.
- **Easy setup**: Grafana and Loki are easy to configure, and you can visualize logs in Grafana alongside Prometheus metrics.

**Setup steps**:

- Fluentd sends logs to Loki over HTTP.
- Grafana queries Loki for logs and visualizes them.


#### Elastic

![[blog-elastic-discover-timestamps.png]]

- **Resource Usage**: Elasticsearch can be resource-intensive, especially with larger data volumes. It may require significant CPU and memory.
- **Elasticsearch** stores and indexes your logs, while **Kibana** provides a web interface for querying and visualizing the data.
- **Fluentd Integration**: Fluentd can easily send logs to Elasticsearch using the `elasticsearch` output plugin. It supports log indexing, querying, and visualizations through Kibana.
- **Easy setup**: Fluentd has native support for sending logs to Elasticsearch, and Kibana is great for log exploration.

**Setup steps**:

- Fluentd sends logs to Elasticsearch over HTTP (port 9200).
- Kibana queries Elasticsearch for logs and provides real-time visualizations and dashboards.

#### Graylog

![[graylog-histogram.png]]

- **Resource Usage**: Graylog is less resource-heavy than ELK, but still requires Elasticsearch and MongoDB, which can add to the system’s load.
- **Graylog** is a centralized log management platform that collects, indexes, and analyzes log data from various sources.
- **Fluentd Integration**: Fluentd can forward logs to Graylog using the **GELF** (Graylog Extended Log Format) output plugin. This allows you to send logs to Graylog over TCP or UDP.
- **Easy setup**: Graylog has an easy-to-use interface with powerful searching and alerting features.

**Setup steps**:

- Fluentd sends logs to Graylog via the GELF protocol.
- Graylog stores logs and provides an interface for querying and visualization.

####  Splunk

![[im-hero-real-time-header.png]]

- **Resource Usage**: Splunk is more resource-intensive than other options due to the powerful indexing and searching capabilities.
- **Splunk** is a powerful platform for machine data analytics, log management, and monitoring.
- **Fluentd Integration**: Fluentd can send logs to Splunk via **HTTP Event Collector (HEC)** or **Splunk Forwarder**. The HEC allows logs to be ingested easily via HTTP POST requests.
- **Easy setup**: Splunk provides a straightforward setup for log forwarding from Fluentd, and their interface is feature-rich with advanced search and analysis capabilities.

**Setup steps**:

- Fluentd sends logs to Splunk via HEC (HTTP Event Collector).
- Splunk processes logs and provides visualizations, search, and alerts.


### Web Developer Environment

Ability to modify the files of the servicies on the web, for quick fixes


## Game

Keep all the data from the current db, layout and lore

## Chat

Utilize (one of them):
- IRC [ergo](https://github.com/ergochat/ergo)
- WebSockets [socket.io](https://github.com/googollee/go-socket.io)

[How to run multiple servers in one process]https://medium.com/rungo/running-multiple-http-servers-in-go-d15300f4e59f()

### IRC vs WebSockets

| Feature                         | WebSockets | IRC |
|---------------------------------|------------|-----|
| **Real-Time Communication**     | ✅ Yes (Low latency) | ✅ Yes (but uses polling) |
| **Modern Web Support**          | ✅ Works in browsers natively | ❌ Requires an external IRC client or web gateway |
| **Persistent Connection**       | ✅ Yes (keeps a connection open) | ✅ Yes (but may require rejoining channels) |
| **Custom Features** (Emojis, Formatting, Bots, etc.) | ✅ Yes (Full control over features) | ⚠️ Limited, depends on the IRC server |
| **Setup Complexity**            | ✅ Simple (Just use WebSockets API) | ❌ Complex (Need to host or connect to an IRC server) |
| **Works on Mobile**             | ✅ Yes | ❌ Not well supported without extra clients |
| **Security (Encryption, Auth, etc.)** | ✅ Supports **SSL/TLS encryption** | ⚠️ Often lacks modern security (some servers support SSL, but not all) |
| **Offline Messages**            | ❌ No (Needs additional backend logic) | ⚠️ Only if using a bouncer (e.g., ZNC) |

### **Why WebSockets is the Better Choice**

✅ **Native Browser Support** – No need for external clients like an IRC client or a gateway.  
✅ **Low Latency** – Messages are sent and received instantly, making it ideal for real-time chat.  
✅ **Customization** – You can implement **custom commands, dice rolls, private messages, and game-specific features**.  
✅ **Better Security** – Supports **TLS encryption**, unlike many traditional IRC servers.  
✅ **Scalability** – Can be hosted on modern backend frameworks (**Node.js, Django, Laravel, etc.**).

---

### **When Would IRC Be Useful?**

❌ **If you want a public chat** across multiple communities.  
❌ **If you prefer an existing chat infrastructure** instead of hosting your own server.  
❌ **If you don't need in-game integration** (but this defeats the purpose in a TRPG app).

Since you’re making a TRPG **web app**, **WebSockets is the best choice** for **integrating chat into your game**. 🎲💬

Would you like a WebSocket chat example to get started? 🚀


## New Features

### Discord integration

### AI

#### AI Images

#### AI Approval

Character and image upload approval

#### AI Lore

AI specialized to create and expand on the current lore.
Could help out the storyteller to come up with new scenarios.

