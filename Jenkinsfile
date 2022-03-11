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
                    publishCoverage adapters: [coberturaAdapter(path: "DiaryLog/TestResults/*/coverage.cobertura.xml")] 
                }
            }
        }
        stage('Deploy') {
            steps {
                echo 'Deploying....'
            }
        }
       }
    }
