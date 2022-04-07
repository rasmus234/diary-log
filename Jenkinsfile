pipeline {
    agent any

    stages {
        stage('Run API Unit Tests') {
            when {
                anyOf {
                    changeset 'DiaryLog/**'
                }
            }

            steps {
                dir('DiaryLog/DiaryLogApiTests') {
                    sh 'sudo rm -rf ./TestResults'
                    sh 'dotnet add package coverlet.collector'
                    sh 'dotnet test --collect:\'XPlat Code Coverage\''
                }
            }

            post {
                success {
                    archiveArtifacts 'DiaryLog/DiaryLogApiTests/TestResults/*/coverage.cobertura.xml'
                    publishCoverage adapters: [coberturaAdapter(path: 'DiaryLog/DiaryLogApiTests/TestResults/*/coverage.cobertura.xml', thresholds: [
                            [failUnhealthy: true, thresholdTarget: 'Conditional', unhealthyThreshold: 80.0, unstableThreshold: 50.0]
                    ])], sourceFileResolver: sourceFiles('NEVER_STORE')
                }
            }
        }

        stage('Build API') {
            steps {
                dir('DiaryLog') {
                    sh 'sudo rm -rf ./bin'
                    sh 'sudo rm -rf ./obj'
                    sh 'dotnet build --configuration Release'
                    sh 'sudo docker-compose --env-file ../config/test.env build api'
                }
            }
        }

        stage('Build Front-end') {
            steps {
                dir('diary-log-angular') {
                    sh 'sudo rm -rf ./dist'
                    sh 'sudo npm install'
                    sh 'sudo ng build'
                }

                sh 'sudo docker-compose --env-file ./config/test.env  build web'
            }
        }

        stage('Clean Containers') {
            steps {
                script {
                    try {
                        sh 'sudo docker-compose --env-file ./config/test.env down'
                    } finally {
                    }
                }
            }
        }

        stage('Deploy') {
            steps {
                sh 'sudo docker-compose -p diary-log --env-file ./config/test.env up -d'
            }
        }

        stage('Push Images To Registry') {
            steps {
                sh 'docker-compose --env-file ./config/test.env push'
            }
        }

        stage('Send Discord Notification') {
            environment {
                WEBHOOK_URL = credentials('DiscordWebhookURL')
            }

            steps {
                discordSend description: 'Build completed', enableArtifactsList: true, footer: '', image: '', link: '', result: 'SUCCESS', scmWebUrl: 'https://github.com/rasmus234/diary-log', showChangeset: true, thumbnail: '', title: 'Diary Log', webhookURL: WEBHOOK_URL
            }
        }
    }
}