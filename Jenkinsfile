pipeline {
    agent any
    stages {
       stage('Build') {
            parallel {
                stage('Build API') {
                    steps {
                        sh 'dotnet build DiaryLog/DiaryLog.sln'
                    }
                }
                stage('Build Front End') {
                    steps {
                        dir('diary-log-angular') {
                            sh 'npm install'
                            sh 'npx ng build diary-log-angular'
                        }
                    }
                }
            }
        }
    
        stage('Test') {
            steps {
                echo 'Testing..'
                dir("DiaryLog/DiaryLogApiTests"){
                    sh "dotnet add package coverlet.collector"
                    sh "dotnet test --collect:'XPlat Code Coverage'"
                }
            }
            post {
                success {
                    publishCoverage adapters: [coberturaAdapter(path: "Diary Log/DiaryLog/DiaryLogApiTests/TestResults")] 
                }
            }
        }
        stage('Deploy') {
            steps {
                echo 'Deploying....'
            }
            post {
                success {
                    discordSend description: 'test', enableArtifactsList: true, footer: '', image: '', link: '', result: '', scmWebUrl: 'https://github.com/rasmus234/diary-log', showChangeset: true, thumbnail: '', title: 'Stigma', webhookURL: 'https://discord.com/api/webhooks/951847255734911016/Vfz5r9qnougI2rLhXfKyleBoToWPTTepmQmB_plN_cf4Fm5VZIYkuoJ4V33haopGg_gb'
                }
            }
        }
       }
    }
