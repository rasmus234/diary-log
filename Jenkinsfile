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
                echo 'Testing API'

                dir("DiaryLog/DiaryLogApiTests") {
                    sh "dotnet test --collect:'XPlat Code Coverage'"
                }

                echo 'Testing front-end'

                dir("diary-log-angular") {
                    sh "npm run test"
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
                echo 'Deploying API'
                sh "sudo su"

                sh "sudo docker rm --force diary-log-api"
                sh 'sudo docker build ./DiaryLog -t diary-log-api'
                sh 'sudo docker run --name diary-log-api -d -p 8060:80 diary-log-api'

                echo 'Deploying front-end'
                sh "sudo docker rm --force diary-log-nginx"
                sh 'sudo docker build ./diary-log-angular -t diary-log'
                sh 'sudo docker run --name diary-log-nginx -d -p 8070:80 diary-log'
            }
            post {
                success {
                    discordSend description: 'test', enableArtifactsList: true, footer: '', image: '', link: '', result: '', scmWebUrl: 'https://github.com/rasmus234/diary-log', showChangeset: true, thumbnail: '', title: 'Stigma', webhookURL: 'https://discord.com/api/webhooks/951847255734911016/Vfz5r9qnougI2rLhXfKyleBoToWPTTepmQmB_plN_cf4Fm5VZIYkuoJ4V33haopGg_gb'
                }
            }
        }
       }
    }
