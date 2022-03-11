pipeline {
    agent any
    stages {
        stage('Build') {
            steps {
                echo 'Building..'

                sh 'dotnet build DiaryLog/DiaryLog.sln'

				dir('diary-log-angular') {
					sh 'npm install'
					sh 'npx ng build diary-log-angular'
				}
            }
        }
        stage('Test') {
            steps {
                echo 'Testing..'
                dir("DiaryLogApiTests"){
                    sh "dotnet add package coverlet.collector"
                    sh "dotnet test --collect:'XPlat Code Coverage'"
                }
            }
            post {
                success {
                    archiveArtifacts(artifacts: '')
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
