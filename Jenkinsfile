pipeline{
    agent any
    stages{
        stage("Build API") {
            when {
                anyOf {
                    changeset "DiaryLog/**"
                }
            }
            steps{
                dir("DiaryLog") {
                    sh "dotnet build --configuration Release"
                }

                sh "sudo docker-compose build api --env-file ./config/test.env"
            }
        }
        stage("Build frontend") {
            when {
                changeset "diary-log-angular/**"
            }
            steps {
                dir("diary-log-angular") {
					sh "sudo rm -rf dist"
					sh "sudo ng build"
                    sh "sudo docker-compose build web"
                }
            }
        }
        stage("Unit Tests") {
            steps {
                echo "Running tests"
                dir("DiaryLog/DiaryLogApiTests") {
                    sh "rm -rf TestResults"
                    sh "dotnet add package coverlet.collector"
                    sh "dotnet test --collect:'XPlat Code Coverage'"
                }
            }
            post {
                success {
                    archiveArtifacts "DiaryLog/DiaryLogApiTests/TestResults/*/coverage.cobertura.xml"
                    publishCoverage adapters: [coberturaAdapter(path: 'DiaryLog/DiaryLogApiTests/TestResults/*/coverage.cobertura.xml', thresholds: [[failUnhealthy: true, thresholdTarget: 'Conditional', unhealthyThreshold: 80.0, unstableThreshold: 50.0]])], sourceFileResolver: sourceFiles('NEVER_STORE')
                }
            }
        }
        stage("Clean containers") {
            steps {
                script {
                    try {
                        sh "sudo docker-compose down --env-file ./config/test.env"
                    }
                    finally { }
                }
            }
        }
        stage("Registry") {
            steps {
                sh "docker-compose --env-file ./config/test.env up -d docker-registry"
                sh "docker-compose --env-file ./config/test.env push"
            }
        }
        stage("Deploy") {
            steps {
                sh "sudo docker-compose up -d --env-file ./config/test.env"
            }
        }
        stage("Discord Notification") {
            steps {
                withCredentials([string(credentialsId: 'DiscordWebhookURL', variable: 'WEBHOOK_URL')]) {
                    discordSend description: 'Build completed', enableArtifactsList: true, footer: '', image: '', link: '', result: 'SUCCESS', scmWebUrl: 'https://github.com/rasmus234/diary-log', showChangeset: true, thumbnail: '', title: 'Diary Log', webhookURL: "${WEBHOOK_URL}"
                }
            }
        }
    }
}